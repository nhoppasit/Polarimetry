using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Agilent.TMFramework.Lxi.Event;

namespace LxiEventManagerSampleApp
{
    public partial class LxiEventManagerSampleApp : Form
    {
        
        public LxiEventManagerSampleApp()
        {
            InitializeComponent();
        }

        // This is the LxiEventManager used by this application
        private LxiEventManager m_lxiEventManager;
 
        private void LxiEventManagerSampleApp_Load(object sender, EventArgs e)
        {
            // Populate the EventIdComboBox with standard LXI event ID's.
            EventIdComboBox.Items.Clear();
            EventIdComboBox.Items.Add(LxiEventId.LAN0);
            EventIdComboBox.Items.Add(LxiEventId.LAN1);
            EventIdComboBox.Items.Add(LxiEventId.LAN2);
            EventIdComboBox.Items.Add(LxiEventId.LAN3);
            EventIdComboBox.Items.Add(LxiEventId.LAN4);
            EventIdComboBox.Items.Add(LxiEventId.LAN5);
            EventIdComboBox.Items.Add(LxiEventId.LAN6);
            EventIdComboBox.Items.Add(LxiEventId.LAN7);

            // Create the LxiEventManager and sign up for its MessageArrived event
            m_lxiEventManager = new LxiEventManager();
            m_lxiEventManager.MessageArrived += new LxiMessageArrivedEventHandler(m_lxiEventManager_MessageArrived);

            // Start monitoring events received on the default LXI port via UDP multicast or TCP/IP unicast to this PC
            // This application will see all locally-visible LXI Events, including those sent by this application.
            m_lxiEventManager.StartMessageNotification();
        }

        void m_lxiEventManager_MessageArrived(object sender, LxiMessageArrivedEventArgs data)
        {
            if (data != null && data.LxiMessage != null)
            {
                // Add a summary of the event to the EventTextBox and scroll to the bottom
                EventTextBox.SuspendLayout();
                EventTextBox.Text += String.Format("{0}\r\n", data.LxiMessage.ToString());
                EventTextBox.SelectAll();
                EventTextBox.ScrollToCaret();
                EventTextBox.ResumeLayout();

                // NOTE: the summary contains the sending endpoint and received time stamps,
                // but the 'data' parameter also contains these properties should this application want
                // to use that information programmatically
                string eventSender = data.RemoteEndPoint.ToString();
                string receivedTimeStamp = data.ReceivedTimeStamp.ToLocalTimeString();
                bool cameViaUdp = data.ReceivedViaUdp;
            }
        }

        private void SimpleRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            EventIdComboBox.Enabled = SimpleRadioButton.Checked;
        }

        private void SendEventButton_Click(object sender, EventArgs e)
        {
            if (SimpleRadioButton.Checked)
            {
                // Send a simple event, setting only the EventID
                // The event is sent to all local instruments via UDP using the default Lxi address and port.
                m_lxiEventManager.SendEvent(EventIdComboBox.Text);
            }
            if (CustomRadioButton.Checked)
            {
                // Create a custom event
                LxiEventMessage lxiEvent = new LxiEventMessage();

                // Set the heading for an error Lxi event
                // (Generally, LxiEventMessage should use the defaults for Heading.Domain and Heading.HWDetect)
                
                // The standard LXIERROR id should always have the Error flag set, and vice versa.
                // Other EventID's don't need the error flag set.
                lxiEvent.EventID = LxiEventId.LXIERROR;
                lxiEvent.Flags = LxiEventFlags.Error;

                // Get the next sequence number for the default LXI destination and domain
                // (each destination path + domain combination is required to use incrementing sequence numbers for successive events)
                lxiEvent.Sequence = m_lxiEventManager.GetNextLxiSequenceId(
                    DestinationPath.DefaultDestinationPath,
                    LxiEventMessage.DefaultDomain);

                // Set the data fields (most instrument events won't contain these, and most instruments will ignore these)
                LxiData dataField = new LxiData(new byte[] { 0, 1, 2 }, 255);
                lxiEvent.Data = new LxiData[1];
                lxiEvent.Data[0] = dataField;

                // Set the timestamp to 'now' as the PC understands it
                // (Unless there is a 1588 clock on the PC that LxiEventTime knows about, 
                //  this won't be a 1588-synchronized time)
                lxiEvent.Time = LxiEventTime.Now;

                // Send the custom event to the default LXI address and port.
                // There are other SendEvent overloads that allow specifying the destination
                m_lxiEventManager.SendEvent(lxiEvent);
            }
        }
    }
}