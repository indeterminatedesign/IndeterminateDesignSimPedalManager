using System;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace IndeterminateDesignSimPedalManager
{


    // Configuration-related structs and helpers have been moved to ConfigurationUtils.cs
    public partial class Form1 : Form
    {
        private SerialPort _pedalsPort;
        private bool pedalsConnected = false;
        private CancellationTokenSource? _cts;
        private Task? _pollingTask;
        private readonly SemaphoreSlim _serialLock = new(1, 1);
        protected int maxThrottlePosition;
        protected int minThrottlePosition;
        protected int maxBrakePosition;
        protected int minBrakePosition;
        protected int minClutchPosition;
        protected int maxClutchPosition;
        protected int currentThrottle;
        protected int currentBrake;
        protected int currentClutch;
        protected int currentRawThrottle;
        protected int currentRawBrake;
        protected int currentRawClutch;
        ConfigurationData configData;

        public Form1()
        {
            InitializeComponent();
            // Initialize configuration with defaults
            configData = ConfigurationUtils.CreateDefaultConfiguration();
            settingsUserControl1.CurveName = "Throttle";

        }



        protected override void OnFormClosing(FormClosingEventArgs e)
        {

            base.OnFormClosing(e);
        }

        private async void buttonScanAndConnect_Click(object sender, EventArgs e)
        {
            string[] testPorts = SerialPort.GetPortNames();

            foreach (string testPort in testPorts)
            {
                SerialPort serial = new SerialPort(testPort, 115200)
                {
                    ReadTimeout = 150,
                    WriteTimeout = 150
                };

                try
                {
                    serial.Open();

                    serial.DtrEnable = true;
                    serial.RtsEnable = true;

                    await Task.Delay(100); // Non-blocking async delay
                    serial.DiscardInBuffer();

                    serial.WriteLine("?");
                    await Task.Delay(100);

                    string response = serial.ReadLine().Trim();

                    if (response == "Indeterminate Design Sim Pedals v1.0")
                    {
                        _pedalsPort = serial; // Hand over the reference securely
                        pedalsConnected = true;


                        debugAppend( $"Successfully connected to pedals on port {testPort}!");
                        StartAsyncPolling();
                        await LoadConfigurationFromPedalsAsync();

                        return; // Exit the loop immediately, leaving the port open
                    }
                }
                catch (Exception)
                {
                    // If connection failed, cleanly dispose only this specific port
                    serial.Dispose();
                }
            }


            debugAppend( "Could not find pedal hardware. Is it plugged in?");
        }


        private void StartAsyncPolling()
        {
            try
            {
                _pedalsPort.DiscardInBuffer();

                // Set up a cancellation token to stop the loop later
                _cts = new CancellationTokenSource();

                // Start the background loop task (Fire and Forget from the UI thread context)
                _pollingTask = PollDeviceAsync(_cts.Token);

                lblStatus.Text = "Connected and Polling...";
            }
            catch (Exception ex)
            {

                debugAppend( $"Failed to open port: {ex.Message}");
            }
        }


        private async Task PollDeviceAsync(CancellationToken cancellationToken)
        {
            // Define a modern 100ms periodic timer
            using var timer = new PeriodicTimer(TimeSpan.FromMilliseconds(100));

            try
            {
                // This loop ticks every 100ms asynchronously without blocking the UI
                while (await timer.WaitForNextTickAsync(cancellationToken))
                {

                    if (_pedalsPort is not { IsOpen: true }) _pedalsPort.Open();

                    //Lock the semaphore to ensure that only one command is sent at a time, preventing overlapping requests
                    await _serialLock.WaitAsync(cancellationToken);
                    try
                    {
                        // 1. Send request asynchronously
                        _pedalsPort.WriteLine("GET_PEDAL_VALUES");

                        // 2. Read response asynchronously line-by-line
                        // .NET's BaseStream allows us to read text from the serial port without blocking
                        using var reader = new StreamReader(_pedalsPort.BaseStream, leaveOpen: true);
                        string? rawData = await reader.ReadLineAsync(cancellationToken);

                        if (string.IsNullOrWhiteSpace(rawData)) continue;

                        if (rawData.StartsWith("$PEDALS:"))
                        {
                            rawData = rawData.Substring(8); // Remove the prefix

                            // 3. Parse the comma-separated data string (e.g., "512,1023")
                            string[] values = rawData.Trim().Split(',');
                            if (values.Length == 6 &&
                                int.TryParse(values[0], out int throttle) &&
                                int.TryParse(values[1], out int brake) &&
                                    int.TryParse(values[2], out int clutch) &&
                                    int.TryParse(values[3], out int rawThrottle) &&
                                    int.TryParse(values[4], out int rawBrake) &&
                                    int.TryParse(values[5], out int rawClutch)
                                    )
                            {
                                // 4. Update the WinForm UI components safely.
                                // Because we are using async/await, .NET automatically handles 
                                // marshaling this back to the main thread safely.

                                if (rawThrottle < maxThrottlePosition)
                                {
                                    maxThrottlePosition = rawThrottle;
                                }
                                if (rawBrake > maxBrakePosition)
                                {
                                    maxBrakePosition = rawBrake;
                                }
                                if (rawClutch > maxClutchPosition)
                                {
                                    maxClutchPosition = rawClutch;
                                }



                                lblStatus.Text = $"CONNECTED -- Throttle: {throttle} | Brake: {brake} | Clutch: {clutch} | Raw Throttle: {rawThrottle} | Raw Brake: {rawBrake} | Raw Clutch: {rawClutch}";
                                lblBrakeRaw.Text = rawBrake.ToString();
                                lblClutchRaw.Text = rawClutch.ToString();
                                lblThrottleRaw.Text = rawThrottle.ToString();
                                lblThrottleMax.Text = maxThrottlePosition.ToString();
                                lblThrottleMin.Text = minThrottlePosition.ToString();
                                lblBrakeMax.Text = maxBrakePosition.ToString();
                                lblBrakeMin.Text = minBrakePosition.ToString();
                                lblClutchMax.Text = maxClutchPosition.ToString();
                                lblClutchMin.Text = minClutchPosition.ToString();

                                currentThrottle = throttle;
                                currentBrake = brake;

                                currentClutch = clutch;
                                currentRawThrottle = rawThrottle;
                                currentRawBrake = rawBrake;
                                currentRawClutch = rawClutch;
                            }
                        }
                    }

                    finally
                    {
                        // Always release the lock so other commands can run!
                        _serialLock.Release();
                    }
                }
            }
            catch (OperationCanceledException)
            {
                // Graceful shutdown triggered by stopping the connection
            }
            catch (Exception ex)
            {
                // Handle unexpected disconnections (e.g., cable pulled out)
                HandleDisconnect($"Error: {ex.Message}");
            }
        }

        // Call this when disconnecting or closing the application
        private async Task StopPollingAsync()
        {
            if (_cts != null)
            {
                await _cts.CancelAsync(); // Request the loop to stop
                _cts.Dispose();
                _cts = null;
            }

            if (_pollingTask != null)
            {
                try { await _pollingTask; } catch { /* Ignore faults on shutdown */ }
                _pollingTask = null;
            }

            if (_pedalsPort is { IsOpen: true })
            {
                _pedalsPort.Close();
                _pedalsPort.Dispose();
            }

            lblStatus.Text = "Disconnected.";
        }

        private void HandleDisconnect(string message)
        {
            // UI cleanup must be done safely on the main thread
            this.BeginInvoke(() =>
            {
                lblStatus.Text = $"Disconnected: {message}";
                _pedalsPort?.Dispose();
            });
        }

        private async Task UploadConfigurationToPedalsAsync(
       ConfigurationData dataToUpload)
        {
            if (_pedalsPort == null || !_pedalsPort.IsOpen)
                return;

            await _serialLock.WaitAsync();

            try
            {
                _pedalsPort.WriteLine("UPDATE_PEDAL_CONFIG");

                _pedalsPort.ReadTimeout = 1000;

                string response = _pedalsPort.ReadLine().Trim();

                if (response != "READY_FOR_DATA")
                {

                    debugAppend( $"Unexpected handshake response: {response}");

                    return;
                }

                byte[] rawBytes =
                    ConfigurationUtils.StructureToByteArray(dataToUpload);

                _pedalsPort.Write(rawBytes, 0, rawBytes.Length);

                while (true)
                {
                    string line;

                    try
                    {
                        line = _pedalsPort.ReadLine().Trim();
                    }
                    catch (TimeoutException)
                    {

                        debugAppend( "Timed out waiting for device response.");

                        return;
                    }

                    //
                    // Log every line
                    //
                    AppendDebugLine(line);

                    //
                    // Success
                    //
                    if (line == "$ACK:SUCCESS")
                    {
                        debugAppend( "Configuration successfully applied!");

                        return;
                    }

                    //
                    // Error
                    //
                    if (line.StartsWith("$ERR:"))
                    {

                        debugAppend( "Error: " + line);

                        return;
                    }
                }
            }
            finally
            {
                _serialLock.Release();
            }
        }

        private async Task SaveConfigurationToFlashAsync()
        {
            if (_pedalsPort == null || !_pedalsPort.IsOpen)
                return;

            await _serialLock.WaitAsync();

            try
            {
                _pedalsPort.WriteLine("SAVE_CONFIG");

                _pedalsPort.ReadTimeout = 2000;

                string response = _pedalsPort.ReadLine().Trim();

                AppendDebugLine($"RX: {response}");

                if (response == "$ACK:SAVED")
                {

                    debugAppend( "Configuration saved to pedal flash memory.");
                }
                else
                {

                    debugAppend( $"Unexpected response: {response}");
                }
            }
            finally
            {
                _serialLock.Release();
            }
        }

        private void AppendDebugLine(string text)
        {
            if (txtDebug.InvokeRequired)
            {
                txtDebug.Invoke(() =>
                    AppendDebugLine(text));

                return;
            }

            txtDebug.AppendText(
                $"[{DateTime.Now:HH:mm:ss.fff}] {text}" +
                Environment.NewLine);

            txtDebug.SelectionStart =
                txtDebug.TextLength;

            txtDebug.ScrollToCaret();
        }

        // Serialization helper moved to ConfigurationUtils.StructureToByteArray

        private void btnWizardNext_Click(object sender, EventArgs e)
        {
            minThrottlePosition = currentRawThrottle;
            minBrakePosition = currentRawBrake;
            minClutchPosition = currentRawClutch;

            //We do this so that the max isn't 0
            maxThrottlePosition = minThrottlePosition;
            maxBrakePosition = minBrakePosition;
            maxClutchPosition = minClutchPosition;
            panelCalibrateInitialPosition.Visible = false;
            panelCalibrateEnd.Visible = true;

        }

        private void tabPageCalibrate_Enter(object sender, EventArgs e)
        {
            panelCalibrateInitialPosition.Visible = true;
            panelCalibrateEnd.Visible = false;
        }

        private void btnFinishCalibration_Click(object sender, EventArgs e)
        {

            tabConfigure.SelectTab(0);
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            saveSettingsAsync();

        }

        private async Task saveSettingsAsync()
        {
            // Load up the struct and calculate absolute ranges
            configData.throttle.startValue = minThrottlePosition;
            configData.throttle.maxValue = maxThrottlePosition;
            configData.throttle.range = Math.Abs(maxThrottlePosition - minThrottlePosition);
            configData.throttle.upperDeadzonePercent = 10;
            configData.throttle.lowerDeadZonePercent = 10;


            configData.brake.startValue = minBrakePosition;
            configData.brake.maxValue = maxBrakePosition;
            configData.brake.range = Math.Abs(maxBrakePosition - minBrakePosition);
            configData.brake.upperDeadzonePercent = 10;
            configData.brake.lowerDeadZonePercent = 10;

            configData.clutch.startValue = minClutchPosition;
            configData.clutch.maxValue = maxClutchPosition;
            configData.clutch.range = Math.Abs(maxClutchPosition - minClutchPosition);
            configData.clutch.upperDeadzonePercent = 10;
            configData.clutch.lowerDeadZonePercent = 10;

            await UploadConfigurationToPedalsAsync(configData);
            await SaveConfigurationToFlashAsync();

        }


        private async Task LoadConfigurationFromPedalsAsync()
        {
            if (_pedalsPort == null || !_pedalsPort.IsOpen) return;

            // 1. Force the background polling loop to yield the serial port
            await _serialLock.WaitAsync();
            try
            {
                // 2. Clear any lingering bytes from the input buffer
                _pedalsPort.DiscardInBuffer();

                // 3. Request the configuration block
                _pedalsPort.WriteLine("GET_PEDAL_CONFIG");

                // 4. Read the ready confirmation from the controller
                _pedalsPort.ReadTimeout = 1000;
                string response = _pedalsPort.ReadLine().Trim();

                if (response == "SENDING_CONFIG")
                {
                    // Calculate exact target size of our ConfigurationData struct (136 bytes)
                    int expectedSize = Marshal.SizeOf<ConfigurationData>();
                    byte[] rawBytes = new byte[expectedSize];
                    int bytesRead = 0;

                    // 5. Read the raw binary bytes synchronously inside our lock block
                    while (bytesRead < expectedSize)
                    {
                        int chunk = _pedalsPort.Read(rawBytes, bytesRead, expectedSize - bytesRead);
                        if (chunk == 0) throw new TimeoutException("Serial connection lost during transfer.");
                        bytesRead += chunk;
                    }

                    // 6. Deserialize raw bytes directly back into our global struct instance
                    configData = ConfigurationUtils.ByteArrayToStructure<ConfigurationData>(rawBytes);

                    // 7. Read final transfer acknowledgment
                    string finalAck = _pedalsPort.ReadLine().Trim();
                    if (finalAck == "$ACK:SUCCESS")
                    {
                        // Update UI elements with our freshly pulled configuration data
                        UpdateUIFromConfig();
                        debugAppend( "Configuration successfully loaded from device!");
                    }
                    else
                    {

                        debugAppend( $"Device returned sync error: {finalAck}");
                    }
                }
                else
                {

                    debugAppend( $"Unexpected handshake response: {response}");
                }
            }
            catch (TimeoutException)
            {

                debugAppend( "The pedal device timed out while transferring configuration data.");
            }
            catch (Exception ex)
            {

                debugAppend( $"Error reading configuration: {ex.Message}");
            }
            finally
            {
                // 8. Yield serial port access back to the 100ms polling thread
                _serialLock.Release();
            }
        }

        private void UpdateUIFromConfig()
        {
            // Make sure UI updates are dispatched back to the main UI thread safely
            this.BeginInvoke(() =>
            {
                // Map throttle values back to UI
                minThrottlePosition = configData.throttle.startValue;
                maxThrottlePosition = configData.throttle.maxValue;
                lblThrottleMin.Text = minThrottlePosition.ToString();
                lblThrottleMax.Text = maxThrottlePosition.ToString();

                // Map brake values back to UI
                minBrakePosition = configData.brake.startValue;
                maxBrakePosition = configData.brake.maxValue;
                lblBrakeMin.Text = minBrakePosition.ToString();
                lblBrakeMax.Text = maxBrakePosition.ToString();

                // Map clutch values back to UI
                minClutchPosition = configData.clutch.startValue;
                maxClutchPosition = configData.clutch.maxValue;
                lblClutchMin.Text = minClutchPosition.ToString();
                lblClutchMax.Text = maxClutchPosition.ToString();
                settingsUserControl1.PedalData = configData.throttle;
                settingsUserControl1.CurrentPedalValue = currentThrottle;



                // Map deadzones, curve indices, and any other visual properties here...
            });
        }

        private void debugAppend(string Message)
        {
            txtDebug.AppendText(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            txtDebug.AppendText("\t");
            txtDebug.AppendText(Message);
            txtDebug.AppendText("\r\n");
        }

    }
}
