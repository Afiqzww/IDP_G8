using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Robot_Arm_Controller_Client
{
    public partial class Form1 : Form
    {
        private TcpClient client;
        private NetworkStream stream;
        private byte[] buffer = new byte[1024];
        private bool isOn = false; // Initial state is off

        public Form1()
        {
            InitializeComponent();
            ConnectionLabel.Text = "Disconnected";
            ON_OFF_ButtonSuction.Text = "OFF";
            ON_OFF_ButtonSuction.BackColor = Color.Red;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Create a TCP client and connect to the server
                client = new TcpClient("10.121.146.166", 5000);

                // Get the network stream for sending and receiving data
                stream = client.GetStream();

                // Start receiving data asynchronously
                stream.BeginRead(buffer, 0, buffer.Length, ReceiveCallback, null);

                ConnectionLabel.Text = "Connected";

                MessageBox.Show("Connected to the server.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void disconnectButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Close the network stream and the TCP client
                stream.Close();
                client.Close();

                ConnectionLabel.Text = "Disconnected";

                MessageBox.Show("Disconnected from the server.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void SendDataToServer(string character)
        {
            try
            {
                // Send the character to the server
                byte[] data = Encoding.UTF8.GetBytes(character.ToString());
                stream.Write(data, 0, data.Length);
                stream.Flush();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void ReceiveCallback(IAsyncResult result)
        {
            try
            {
                // End the asynchronous read operation
                int bytesRead = stream.EndRead(result);

                if (bytesRead > 0)
                {
                    // Convert the received bytes to a string
                    string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                    // Display the response in the UI
                    this.Invoke(new Action(() =>
                    {
                        responseBox.Text += response + Environment.NewLine;
                    }));

                    // Continue receiving data asynchronously
                    stream.BeginRead(buffer, 0, buffer.Length, ReceiveCallback, null);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void P_RailingButton_Click(object sender, EventArgs e)
        {
            SendDataToServer("1");
        }

        private void M_RailingButton_Click(object sender, EventArgs e)
        {
            SendDataToServer("2");
        }

        private void P_Joint1Button_Click(object sender, EventArgs e)
        {
            SendDataToServer("3");
        }

        private void M_Joint1Button_Click(object sender, EventArgs e)
        {
            SendDataToServer("4");
        }

        private void P_Joint2Button_Click(object sender, EventArgs e)
        {
            SendDataToServer("5");
        }

        private void M_Joint2Button_Click(object sender, EventArgs e)
        {
            SendDataToServer("6");
        }

        private void P_Joint3Button_Click(object sender, EventArgs e)
        {
            SendDataToServer("7");
        }

        private void M_Joint3Button_Click(object sender, EventArgs e)
        {
            SendDataToServer("8");
        }

        private void P_Joint4Button_Click(object sender, EventArgs e)
        {
            SendDataToServer("9");
        }

        private void M_Joint4Button_Click(object sender, EventArgs e)
        {
            SendDataToServer("10");
        }

        private void P_Joint5Button_Click(object sender, EventArgs e)
        {
            SendDataToServer("11");
        }

        private void M_Joint5Button_Click(object sender, EventArgs e)
        {
            SendDataToServer("12");
        }

        private void Stop_RailingButton_Click(object sender, EventArgs e)
        {
            SendDataToServer("SR");
        }

        private void Stop_Joint1Button_Click(object sender, EventArgs e)
        {
            SendDataToServer("S1");
        }

        private void Stop_Joint2Button_Click(object sender, EventArgs e)
        {
            SendDataToServer("S2");
        }

        private void Stop_Joint3Button_Click(object sender, EventArgs e)
        {
            SendDataToServer("S3");
        }

        private void Stop_Joint4Button_Click(object sender, EventArgs e)
        {
            SendDataToServer("S4");
        }

        private void Stop_Joint5Button_Click(object sender, EventArgs e)
        {
            SendDataToServer("S5");
        }

        private void ON_OFF_ButtonSuction_Click(object sender, EventArgs e)
        {
            if (isOn)
            {
                // Perform action for turning off
                isOn = false;
                ON_OFF_ButtonSuction.Text = "OFF";
                ON_OFF_ButtonSuction.BackColor = Color.Red;
                SendDataToServer("x");
                // Other code or logic for when the button is turned off
            }
            else
            {
                // Perform action for turning on
                isOn = true;
                ON_OFF_ButtonSuction.Text = "ON";
                ON_OFF_ButtonSuction.BackColor = Color.Green;
                SendDataToServer("X");
                // Other code or logic for when the button is turned on
            }
        }

        private void home_button_Click(object sender, EventArgs e)
        {
            SendDataToServer("H");
        }

        private void StopAll_button_Click(object sender, EventArgs e)
        {
            SendDataToServer("S");
        }

        private void SavePos1_button_Click(object sender, EventArgs e)
        {
            SendDataToServer("O");
        }

        private void Go1_button_Click(object sender, EventArgs e)
        {
            SendDataToServer("o");
        }

        private void SavePos2_button_Click(object sender, EventArgs e)
        {
            SendDataToServer("P");
        }

        private void Go2_button_Click(object sender, EventArgs e)
        {
            SendDataToServer("p");
        }

        private void AdjustmentPosition(char move)
        {
            //Railing
            if ( componentBox.Text== "RAILING" && move == 'm')
            {
                SendDataToServer("MR");
            }
            else if ( componentBox.Text == "RAILING" && move == 'n')
            {
                SendDataToServer("NR");
            }
            else if ( componentBox.Text == "RAILING" && move == 'b')
            {
                SendDataToServer("BR");
            }
            //Joint 1
            else if (componentBox.Text == "JOINT 1" && move == 'm')
            {
                SendDataToServer("M1");
            }
            else if (componentBox.Text == "JOINT 1" && move == 'n')
            {
                SendDataToServer("N1");
            }
            else if (componentBox.Text == "JOINT 1" && move == 'b')
            {
                SendDataToServer("P1");
            }
            //Joint 2
            else if (componentBox.Text == "JOINT 2" && move == 'm')
            {
                SendDataToServer("M2");
            }
            else if (componentBox.Text == "JOINT 2" && move == 'n')
            {
                SendDataToServer("N2");
            }
            else if (componentBox.Text == "JOINT 2" && move == 'b')
            {
                SendDataToServer("B2");
            }
            //JOINT 3
            else if (componentBox.Text == "JOINT 3" && move == 'm')
            {
                SendDataToServer("M3");
            }
            else if (componentBox.Text == "JOINT 3" && move == 'n')
            {
                SendDataToServer("N3");
            }
            else if (componentBox.Text == "JOINT 3" && move == 'b')
            {
                SendDataToServer("B3");
            }
            //JOINT 4
            else if (componentBox.Text == "JOINT 4" && move == 'm')
            {
                SendDataToServer("M4");
            }
            else if (componentBox.Text == "JOINT 4" && move == 'n')
            {
                SendDataToServer("N4");
            }
            else if (componentBox.Text == "JOINT 4" && move == 'b')
            {
                SendDataToServer("B4");
            }
            //JOINT 5
            else if (componentBox.Text == "JOINT 5" && move == 'm')
            {
                SendDataToServer("M5");
            }
            else if (componentBox.Text == "JOINT 5" && move == 'n')
            {
                SendDataToServer("N5");
            }
            else if (componentBox.Text == "JOINT 5" && move == 'b')
            {
                SendDataToServer("B5");
            }

            else
            {
                MessageBox.Show("Something Missing");
            }
        }

        private void P_positionButton_Click(object sender, EventArgs e)
        {
            AdjustmentPosition('m');
        }

        private void M_positionButton_Click(object sender, EventArgs e)
        {
            AdjustmentPosition('n');
        }

        private void Stop_positionButton_Click(object sender, EventArgs e)
        {
            AdjustmentPosition('b');
        }

        private void ResetPos_button_Click(object sender, EventArgs e)
        {
            SendDataToServer("R");
        }
    } 
}
