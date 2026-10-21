using Microsoft.VisualBasic;
using System;
using System.IO.Ports;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

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
        private bool calibrationActive = false;
        private bool brakeMaxManuallyEdited;
        private bool updatingBrakeMaxTextBox;

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
            settingsThrottle.CurveName = "Throttle";
            settingsBrake.CurveName = "Brake";
            settingsClutch.CurveName = "Clutch";

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


                        debugAppend($"Successfully connected to pedals on port {testPort}!");
                        tabConfigure.Enabled = true;
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


            debugAppend("Could not find pedal hardware. Is it plugged in?");
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

                debugAppend($"Failed to open port: {ex.Message}");
            }
        }


        private async Task PollDeviceAsync(CancellationToken cancellationToken)
        {
            // Define a modern 50ms periodic timer
            using var timer = new PeriodicTimer(TimeSpan.FromMilliseconds(50));

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
                                int.TryParse(values[0], out int rawThrottle) &&
                                int.TryParse(values[1], out int throttle) &&
                                    int.TryParse(values[2], out int rawBrake) &&
                                    int.TryParse(values[3], out int brake) &&
                                    int.TryParse(values[4], out int rawClutch) &&
                                    int.TryParse(values[5], out int clutch)
                                    )
                            {
                                // 4. Update the WinForm UI components safely.
                                // Because we are using async/await, .NET automatically handles 
                                // marshaling this back to the main thread safely.


                                lblStatus.Text = $"CONNECTED -- Raw Throttle: {rawThrottle} | Throttle: {throttle} | Raw Brake: {rawBrake}  | Brake: {brake} | Raw Clutch: {rawClutch} | Clutch: {clutch} ";

                                if (calibrationActive)
                                {
                                    if (configData.throttle.maxValue > rawThrottle)
                                    {
                                        lblThrottleMax.Text = rawThrottle.ToString();
                                        configData.throttle.maxValue = rawThrottle;
                                    }
                                    if (configData.brake.maxValue < rawBrake)
                                    {
                                        configData.brake.maxValue = rawBrake;
                                        if (!brakeMaxManuallyEdited)
                                        {
                                            updatingBrakeMaxTextBox = true;
                                            lblBrakeMax.Text = rawBrake.ToString();
                                            updatingBrakeMaxTextBox = false;
                                        }
                                    }
                                    if (configData.clutch.maxValue > rawClutch)
                                    {
                                        lblClutchMax.Text = rawBrake.ToString();
                                        configData.clutch.maxValue = rawClutch;
                                    }
                                }

                                currentThrottle = throttle;
                                currentBrake = brake;

                                currentClutch = clutch;
                                currentRawThrottle = rawThrottle;
                                currentRawBrake = rawBrake;
                                currentRawClutch = rawClutch;

                                //Calculate the current pedal position as a value from 0-100%
                                float throttlePercent = (Math.Abs(Convert.ToSingle(currentRawThrottle - configData.throttle.startValue)) / configData.throttle.range) * 100.0f;
                                if (throttlePercent >= 0 && throttlePercent <= 100)
                                {
                                    settingsThrottle.CurrentPedalPercent = Convert.ToInt16(throttlePercent);
                                }
                                float brakePercent = (Math.Abs(Convert.ToSingle(currentRawBrake - configData.brake.startValue)) / configData.brake.range) * 100.0f;
                                if (brakePercent >= 0 && brakePercent <= 100)
                                {
                                    settingsBrake.CurrentPedalPercent = Convert.ToInt16(brakePercent);
                                }
                                float clutchPercent = (Math.Abs(Convert.ToSingle(currentRawClutch - configData.clutch.startValue)) / configData.clutch.range) * 100.0f;
                                if (clutchPercent >= 0 && clutchPercent <= 100)
                                {
                                    settingsClutch.CurrentPedalPercent = Convert.ToInt16(clutchPercent);
                                }

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

        private void HandleDisconnect(string message)
        {
            // UI cleanup must be done safely on the main thread
            this.BeginInvoke(() =>
            {
                lblStatus.Text = $"Disconnected: {message}";
                _pedalsPort?.Dispose();
            });
        }



        private async Task UploadConfigurationToPedalsAsync(ConfigurationData dataToUpload)
        {
            if (_pedalsPort == null || !_pedalsPort.IsOpen) return;

            // 1. Force background telemetry polling loop to yield access to the serial pipeline channel
            await _serialLock.WaitAsync();
            try
            {
                // 2. Clear out any unread noise from the hardware input stream queue
                _pedalsPort.DiscardInBuffer();

                // 3. Configure string serialization properties to output clean camelCase variables matching ArduinoJson names
                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                string jsonPayload = JsonSerializer.Serialize(dataToUpload, options);

                // 4. Dispatch the entire text payload down to the controller in a single transaction line
                _pedalsPort.WriteLine($"$UPDATE_CONFIG:{jsonPayload}");
                _pedalsPort.ReadTimeout = 1500; // Giving the controller slightly more room to process the text parsing block

                // 5. Monitor lines returning back over the virtual COM port channel
                while (true)
                {
                    string line;
                    try
                    {
                        line = _pedalsPort.ReadLine().Trim();
                    }
                    catch (TimeoutException)
                    {
                        debugAppend("Timed out waiting for device response.");
                        return;
                    }

                    // Log output tracking line to the debug view window
                    AppendDebugLine(line);

                    // SUCCESS HANDSHAKE: Triggered when Arduino parses all 41 fields successfully
                    if (line == "$ACK:CONFIG_UPDATED")
                    {
                        debugAppend("Configuration successfully applied to temporary microcontroller RAM!");
                        return;
                    }

                    // ERROR ESCAPE VALVE: Catch signature mismatches or parsing runtime blocks
                    if (line.StartsWith("$ERR:"))
                    {
                        debugAppend("Error: " + line);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                debugAppend($"Error transmitting settings configuration payload: {ex.Message}");
            }
            finally
            {
                // 6. Release lock, restoring normal runtime dashboard telemetry collection reads
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

                    debugAppend("Configuration saved to pedal flash memory.");
                }
                else
                {

                    debugAppend($"Unexpected response: {response}");
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
            calibrationActive = true;

            configData.throttle.startValue = currentRawThrottle;
            configData.brake.startValue = currentRawBrake;
            configData.clutch.startValue = currentRawClutch;

            //We need to set the max values to the min value so we can track it to when the pedal moves to the end of it's ranges
            configData.throttle.maxValue = currentRawThrottle;
            configData.brake.maxValue = currentRawBrake;
            configData.clutch.maxValue = currentRawClutch;

            lblThrottleMin.Text = configData.throttle.startValue.ToString();
            lblBrakeMin.Text = configData.brake.startValue.ToString();
            lblClutchMin.Text = configData.clutch.startValue.ToString();

            //We do this so that the max isn't 0, we want the max to start as low (or high) as possible and then track from there
            lblThrottleMax.Text = configData.throttle.startValue.ToString();
            brakeMaxManuallyEdited = false;
            updatingBrakeMaxTextBox = true;
            lblBrakeMax.Text = configData.brake.startValue.ToString();
            updatingBrakeMaxTextBox = false;
            lblClutchMax.Text = configData.clutch.startValue.ToString();

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
            if (!int.TryParse(lblBrakeMax.Text, out int brakeMax))
            {
                MessageBox.Show("Enter a valid brake value.", "Invalid brake value", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                lblBrakeMax.Focus();
                return;
            }

            configData.brake.maxValue = brakeMax;
            calibrationActive = false;
            tabConfigure.SelectTab(0);
        }

        private void lblBrakeMax_TextChanged(object sender, EventArgs e)
        {
            if (!updatingBrakeMaxTextBox)
            {
                brakeMaxManuallyEdited = true;
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            LoadConfigDataObjectFromControls();

            saveSettingsAsync();

        }

        private void btnSaveProfile_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(lblBrakeMax.Text, out int brakeMax))
            {
                MessageBox.Show("Enter a valid brake value before saving the profile.", "Invalid brake value", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                lblBrakeMax.Focus();
                return;
            }

            configData.brake.maxValue = brakeMax;
            LoadConfigDataObjectFromControls();

            string profilesDirectory = Path.Combine(AppContext.BaseDirectory, "Profiles");
            Directory.CreateDirectory(profilesDirectory);

            using SaveFileDialog dialog = new()
            {
                InitialDirectory = profilesDirectory,
                Filter = "Pedal profile (*.json)|*.json|All files (*.*)|*.*",
                DefaultExt = "json",
                AddExtension = true,
                FileName = "PedalProfile.json",
                Title = "Save Pedal Profile"
            };

            if (dialog.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            try
            {
                File.WriteAllText(dialog.FileName, ConfigurationUtils.SerializeProfile(configData));
                debugAppend($"Profile saved to {dialog.FileName}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unable to save the profile: {ex.Message}", "Save Profile Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task saveSettingsAsync()
        {
            await UploadConfigurationToPedalsAsync(configData);
            await SaveConfigurationToFlashAsync();

        }

        private void LoadConfigDataObjectFromControls()
        {
            configData.throttle.inputToOutputRatios = (short[])settingsThrottle.InputToOutputRatios.Clone();
            configData.brake.inputToOutputRatios = (short[])settingsBrake.InputToOutputRatios.Clone();
            configData.clutch.inputToOutputRatios = (short[])settingsClutch.InputToOutputRatios.Clone();
            // Calculate the Range of travel total
            configData.throttle.range = Math.Abs(configData.throttle.maxValue - configData.throttle.startValue);
            configData.brake.range = Math.Abs(configData.brake.maxValue - configData.brake.startValue);
            configData.clutch.range = Math.Abs(configData.clutch.maxValue - configData.clutch.startValue);
        }


        private async Task LoadConfigurationFromPedalsAsync()
        {
            if (_pedalsPort == null || !_pedalsPort.IsOpen) return;

            await _serialLock.WaitAsync();
            try
            {
                _pedalsPort.DiscardInBuffer();
                _pedalsPort.WriteLine("GET_PEDAL_CONFIG");

                _pedalsPort.ReadTimeout = 1500;
                string jsonLine = _pedalsPort.ReadLine().Trim();

                // ADDED: Log the raw incoming JSON string directly to your UI debug panel
                debugAppend($"Raw Incoming JSON: {jsonLine}");

                // Magic Option: This tells C# to read "startValue" and map it to "StartValue" perfectly 
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                configData = JsonSerializer.Deserialize<ConfigurationData>(jsonLine, options);

                UpdateUIFromConfig();
                debugAppend("Configuration successfully loaded and parsed via JSON!");
            }
            catch (Exception ex)
            {
                debugAppend($"Error loading JSON configuration: {ex.Message}");
            }
            finally
            {
                _serialLock.Release();
            }
        }



        private void UpdateUIFromConfig()
        {
            void updateControls()
            {
                lblThrottleMin.Text = configData.throttle.startValue.ToString();
                lblBrakeMin.Text = configData.brake.startValue.ToString();
                lblClutchMin.Text = configData.clutch.startValue.ToString();
                lblThrottleMax.Text = configData.throttle.maxValue.ToString();
                lblClutchMax.Text = configData.clutch.maxValue.ToString();
                updatingBrakeMaxTextBox = true;
                lblBrakeMax.Text = configData.brake.maxValue.ToString();
                updatingBrakeMaxTextBox = false;
                brakeMaxManuallyEdited = false;

                settingsThrottle.LoadFromPedalData(configData.throttle);
                settingsBrake.LoadFromPedalData(configData.brake);
                settingsClutch.LoadFromPedalData(configData.clutch);
            }

            if (InvokeRequired)
            {
                BeginInvoke(updateControls);
            }
            else
            {
                updateControls();
            }
        }

        private void debugAppend(string Message)
        {
            txtDebug.AppendText(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            txtDebug.AppendText("\t");
            txtDebug.AppendText(Message);
            txtDebug.AppendText("\r\n");
        }

        private void btnLoadSettingsFromProfile_Click(object sender, EventArgs e)
        {
            string profilesDirectory = Path.Combine(AppContext.BaseDirectory, "Profiles");
            Directory.CreateDirectory(profilesDirectory);

            using OpenFileDialog dialog = new()
            {
                InitialDirectory = profilesDirectory,
                Filter = "Pedal profile (*.json)|*.json|All files (*.*)|*.*",
                DefaultExt = "json",
                Title = "Load Pedal Profile"
            };

            if (dialog.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            try
            {
                configData = ConfigurationUtils.DeserializeProfile(File.ReadAllText(dialog.FileName));
                UpdateUIFromConfig();
                debugAppend($"Profile loaded from {dialog.FileName}");
            }
            catch (Exception ex) when (ex is IOException || ex is JsonException || ex is InvalidOperationException)
            {
                MessageBox.Show($"Unable to load the profile: {ex.Message}", "Load Profile Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
