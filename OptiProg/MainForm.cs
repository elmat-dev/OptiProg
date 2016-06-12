/*
 * Created by SharpDevelop.
 * User: g.chmiel
 * Date: 2016-06-10
 * Time: 10:57
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Timers;

using HidSharp;

namespace OptiProg
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		private static System.Timers.Timer aTimer;
		HidDeviceLoader loader = new HidDeviceLoader();
		public byte [] send = new byte[64];
		public byte [] receive = new byte[64];
		public HidStream stream;
		
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			aTimer = new System.Timers.Timer(200);
			aTimer.Elapsed += OnTimedEvent;
			aTimer.AutoReset = true;
			aTimer.Enabled = true;

			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//

						
			
		/*
		  			for (int i = 0; i < dataGridView2.Rows.Count; ++i)
     		{
         		DataGridViewComboBoxCell cell = dataGridView2.Rows[i].Cells[6] as DataGridViewComboBoxCell;
       
		             cell.Items.Clear();
		             cell.Items.Add("one");
		             cell.Items.Add("two");

		     }
		*/
		dataGridView2.RowTemplate.Height = 18;

		for (int i=0;i<128;i++)
			dataGridView2.Rows.Add();
		for (int i=0;i<128;i++) {
			dataGridView2.Rows[i].Cells[0].Value = i;
			dataGridView2.Rows[i].Cells[1].Value = i;
		}
		for (int i=20;i<36;i++) {
			dataGridView2.Rows[i].Cells[0].Style.BackColor = Color.LightGreen;
			dataGridView2.Rows[i].Cells[1].Style.BackColor = Color.LightGreen;
			dataGridView2.Rows[i].Cells[2].Style.BackColor = Color.LightGreen;
			dataGridView2.Rows[i].Cells[3].Style.BackColor = Color.LightGreen;
			dataGridView2.Rows[i].Cells[4].Style.BackColor = Color.LightGreen;
			dataGridView2.Rows[i].Cells[5].Style.BackColor = Color.LightGreen;
			dataGridView2.Rows[i].Cells[6].Style.BackColor = Color.LightGreen;
		}
		for (int i=40;i<56;i++) {
			dataGridView2.Rows[i].Cells[0].Style.BackColor = Color.SkyBlue;
			dataGridView2.Rows[i].Cells[1].Style.BackColor = Color.SkyBlue;
			dataGridView2.Rows[i].Cells[2].Style.BackColor = Color.SkyBlue;
			dataGridView2.Rows[i].Cells[3].Style.BackColor = Color.SkyBlue;
			dataGridView2.Rows[i].Cells[4].Style.BackColor = Color.SkyBlue;
			dataGridView2.Rows[i].Cells[5].Style.BackColor = Color.SkyBlue;
			dataGridView2.Rows[i].Cells[6].Style.BackColor = Color.SkyBlue;
		}
		for (int i=68;i<84;i++) {
			dataGridView2.Rows[i].Cells[0].Style.BackColor = Color.LightCoral;
			dataGridView2.Rows[i].Cells[1].Style.BackColor = Color.LightCoral;
			dataGridView2.Rows[i].Cells[2].Style.BackColor = Color.LightCoral;
			dataGridView2.Rows[i].Cells[3].Style.BackColor = Color.LightCoral;
			dataGridView2.Rows[i].Cells[4].Style.BackColor = Color.LightCoral;
			dataGridView2.Rows[i].Cells[5].Style.BackColor = Color.LightCoral;
			dataGridView2.Rows[i].Cells[6].Style.BackColor = Color.LightCoral;
		}
		
	}

		
	
		
		
		
		
		


		void OnTimedEvent(Object source, ElapsedEventArgs e)												// it's and event which is checking for device presence in the system, fired by timer set in main application currently
		{
			var device = loader.GetDevices(1155, 22352).FirstOrDefault();								// check if device is present
			if (device == null) {																		// if it is not present in the system then:
				toolStripStatusLabel1.ForeColor = Color.Red;											// change status bar label font color to RED and
				toolStripStatusLabel1.Text = "Device not detected";										// set status bar label text accordingly
			}
			
			else {																						// if it is present in the system then:
				toolStripStatusLabel1.ForeColor = Color.Green;											// change status bar label font color to GREEN and
				toolStripStatusLabel1.Text = "Device detected";											// set status bar label text accordingly
			}
			

			device.TryOpen (out stream);
			stream.Read(receive);

			if ( receive[2] == 1 ){
				toolStripStatusLabel2.Text = "Button pressed";	
				Thread.Sleep(100);
			}
			receive[2] = 0;
				toolStripStatusLabel2.Text = " ";

			this.statusStrip1.Update();																		// update status bar label text to show changes

		}
		
		
		void Button2Click(object sender, EventArgs e)
		{
			var device = loader.GetDevices(1155, 22352).FirstOrDefault();
  			device.TryOpen(out stream);
  			send[3] = 0x33;
  			//send[4] = 0;
  			stream.Write(send); 

  			//toolStripStatusLabel2.Text = " Button 2";
			//statusStrip1.Refresh();				
		}
		void Button3Click(object sender, EventArgs e)
		{
			var device = loader.GetDevices(1155, 22352).FirstOrDefault();
  			device.TryOpen(out stream);
  			send[3] = 0x34;
  			//send[4] = 0;
  			stream.Write(send); 
  			//toolStripStatusLabel2.Text = " Button 3";
			//statusStrip1.Refresh();		
		}
		void Button4Click(object sender, EventArgs e)
		{
			//var device = loader.GetDevices(1155, 22352).FirstOrDefault();
  			//device.TryOpen (out stream);
  			//stream.Read(receive);

  			//if (receive[2]==1) MessageBox.Show("Button press detected!");
   
  		
						var device = loader.GetDevices(1155, 22352).FirstOrDefault();
  			device.TryOpen(out stream);
			send[4]= byte.Parse(textBox1.Text);
  			stream.Write(send); 
  			


  			
		}

		void Button5Click(object sender, EventArgs e)
		{
			

		}
		


		void MainFormLoad(object sender, EventArgs e)
		{
	
		}
		void DataGridView2CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
	
		}
		void Button1Click(object sender, EventArgs e)
		{
			openFileDialog1.Title = "Open File...";
            openFileDialog1.Filter = "Binary File (*.bin)|*.bin";
            openFileDialog1.InitialDirectory = @"C:\";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var fs = new FileStream(openFileDialog1.FileName, FileMode.Open);
                var br = new BinaryReader(fs);

				for (int i=0;i<128;i++) {
					var temp = new byte();
					temp = br.ReadByte();
					dataGridView2.Rows[i].Cells[2].Value = temp;
					dataGridView2.Rows[i].Cells[3].Value = temp;
					dataGridView2.Rows[i].Cells[4].Value = Convert.ToChar(temp);
				}
                fs.Close();
                br.Close();
               
            }
		}
		

		
		
		
		
		
		
		
	}
}
