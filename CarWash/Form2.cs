using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarWash
{
    public partial class Form2 : Form
    {

        CancellationTokenSource tcsHall1;
        CancellationTokenSource tcsHall2;
        CancellationTokenSource tcsHall3;
        CancellationTokenSource tcsHall4;
        SynchronizationContext ctx = null;
        bool IgangHall1 = false;
        bool IgangHall2 = false;
        bool IgangHall3 = false;
        bool IgangHall4 = false;
        public Form2()
        {
            InitializeComponent();
        }

        #region Start Button
        //Start button gathers information on click what hall and wash type was chosen and sends it to the car was methods
        private async void btnStart_Click(object sender, EventArgs e)
        {
            ctx = SynchronizationContext.Current;
            Parameters param = new Parameters();
            param.Vasktype = cmbBoxVasktype.SelectedIndex;
            if (cmbBoxHaller.SelectedIndex == 1)
            {
                tcsHall1 = new CancellationTokenSource();
                param.ct = tcsHall1.Token;
                ThreadPool.QueueUserWorkItem(Hall1Vask, param);
            }
            else if (cmbBoxHaller.SelectedIndex == 2)
            {
                tcsHall2 = new CancellationTokenSource();
                param.ct = tcsHall2.Token;
                Task t = Task.Factory.StartNew(Hall2Vask, param);
            }
            else if (cmbBoxHaller.SelectedIndex == 3)
            {
                tcsHall3 = new CancellationTokenSource();
                param.ct = tcsHall3.Token;
                Task t = Task.Run(() => Hall3Vask(param));
            }
            else if (cmbBoxHaller.SelectedIndex == 4)
            {
                tcsHall4 = new CancellationTokenSource();
                param.ct = tcsHall4.Token;
                await Task.Run(() => Hall4Vask(param));
            }
            else if (cmbBoxHaller.SelectedIndex == 0)
            {
                MessageBox.Show("Du skal vælge en vaskehall!", "Fejl");
            }
        }
        #endregion

        #region Car Wash Methods
        //Methods that control the single wash halls
        void Hall1Vask(Object data)
        {
            try
            {
                Parameters parameters = (Parameters)data;
                parameters.HallNr = 1;
                ctx.Send(status => cmbBoxVasktype.SelectedIndex = 0, null);
                //The main progression part of the programm sets label texts and colors according to how far the program is
                if (!IgangHall1)
                {
                    IgangHall1 = true;
                    ctx.Send(status => btnHall1Cancel.Visible = true, null);
                    ctx.Send(status => cmbBoxHaller.SelectedIndex = 0, null);
                    ctx.Send(status => lblHALL1status.Text = "I GANG", null);
                    lblHALL1status.BackColor = Color.Orange;
                    lblHALL1iblø.BackColor = Color.Yellow;
                    lblHALL1vask.BackColor = Color.Orange;
                    if (parameters.Vasktype == 1 || parameters.Vasktype == 2)
                    {
                        lblHALL1uvask.BackColor = Color.Orange;
                    }
                    lblHALL1skyl.BackColor = Color.Orange;
                    lblHALL1tør.BackColor = Color.Orange;
                    if (parameters.Vasktype == 1)
                    {
                        lblHALL1voks.BackColor = Color.Orange;
                    }
                    ProgressionBar(parameters, 500);
                    lblHALL1iblø.BackColor = Color.Green;
                    lblHALL1vask.BackColor = Color.Yellow;
                    ProgressionBar(parameters, 500);
                    lblHALL1vask.BackColor = Color.Green;
                    if (parameters.Vasktype == 1 || parameters.Vasktype == 2)
                    {
                        lblHALL1uvask.BackColor = Color.Yellow;
                        ProgressionBar(parameters, 500);
                        lblHALL1uvask.BackColor = Color.Green;
                    }
                    lblHALL1skyl.BackColor = Color.Yellow;
                    ProgressionBar(parameters, 500);
                    lblHALL1skyl.BackColor = Color.Green;
                    lblHALL1tør.BackColor = Color.Yellow;
                    ProgressionBar(parameters, 500);
                    lblHALL1tør.BackColor = Color.Green;
                    if (parameters.Vasktype == 1)
                    {
                        lblHALL1voks.BackColor = Color.Yellow;
                        ProgressionBar(parameters, 500);
                        lblHALL1voks.BackColor = Color.Green;
                    }
                    lblHALL1status.BackColor = Color.Yellow;
                    ctx.Send(status => lblHALL1status.Text = "GØR KLAR", null);
                    ProgressionBar(parameters, 500);
                    lblHALL1iblø.BackColor = Color.Gray;
                    lblHALL1vask.BackColor = Color.Gray;
                    lblHALL1uvask.BackColor = Color.Gray;
                    lblHALL1skyl.BackColor = Color.Gray;
                    lblHALL1tør.BackColor = Color.Gray;
                    lblHALL1voks.BackColor = Color.Gray;
                    ProgressionBar(parameters, 500);
                    ctx.Send(status => lblHALL1status.Text = "KLAR", null);
                    lblHALL1status.BackColor = Color.Green;
                    IgangHall1 = false;
                }
                //Shows a pop-up window with an error message if the user has not chosen a wash typeS
                else if (parameters.Vasktype == 0 && !IgangHall1)
                {
                    ctx.Send(status => cmbBoxHaller.SelectedIndex = 0, null);
                    lblErrorMsg.BackColor = Color.Red;
                    ctx.Send(status => lblErrorMsg.Text = "Du skal vælge en Vasktype", null);
                    Thread.Sleep(5000);
                    lblErrorMsg.BackColor = Color.Transparent;
                    ctx.Send(status => lblErrorMsg.Text = "", null);
                }
                //If the hall is already occupied with a task sends an error message to the "lblErrorMsg" label
                else
                {
                    lblErrorMsg.BackColor = Color.Red;
                    ctx.Send(status => lblErrorMsg.Text = "Hall 1 er allerede igang", null);
                    Thread.Sleep(5000);
                    lblErrorMsg.BackColor = Color.Transparent;
                    ctx.Send(status => lblErrorMsg.Text = "", null);
                }
                ctx.Send(status => btnHall1Cancel.Visible = false, null);
                ctx.Send(status => pgbHall1.Value = 0, null);
            }
            //Catches the exception triggered by the press of the Cancelation button triggers the Cancelation Event
            catch (Exception ex)
            {
                Task t = Task.Run(() => CancelationEvent(1, ex.Message));
            }

        }
        void Hall2Vask(object data)
        {
            try
            {
                Parameters parameters = (Parameters)data;
                parameters.HallNr = 2;
                ctx.Send(status => cmbBoxVasktype.SelectedIndex = 0, null);
                parameters.ct.ThrowIfCancellationRequested();
                if (!IgangHall2)
                {
                    IgangHall2 = true;
                    ctx.Send(status => btnHall2Cancel.Visible = true, null);
                    ctx.Send(status => cmbBoxHaller.SelectedIndex = 0, null);
                    ctx.Send(status => lblHALL2status.Text = "I GANG", null);
                    lblHALL2status.BackColor = Color.Orange;
                    lblHALL2iblø.BackColor = Color.Yellow;
                    lblHALL2vask.BackColor = Color.Orange;
                    if (parameters.Vasktype == 1 || parameters.Vasktype == 2)
                    {
                        lblHALL2uvask.BackColor = Color.Orange;
                    }
                    lblHALL2skyl.BackColor = Color.Orange;
                    lblHALL2tør.BackColor = Color.Orange;
                    if (parameters.Vasktype == 1)
                    {
                        lblHALL2voks.BackColor = Color.Orange;
                    }
                    ProgressionBar(parameters, 500);
                    lblHALL2iblø.BackColor = Color.Green;
                    lblHALL2vask.BackColor = Color.Yellow;
                    ProgressionBar(parameters, 500);
                    lblHALL2vask.BackColor = Color.Green;
                    if (parameters.Vasktype == 1 || parameters.Vasktype == 2)
                    {
                        lblHALL2uvask.BackColor = Color.Yellow;
                        ProgressionBar(parameters, 500);
                        lblHALL2uvask.BackColor = Color.Green;
                    }
                    lblHALL2skyl.BackColor = Color.Yellow;
                    ProgressionBar(parameters, 500);
                    lblHALL2skyl.BackColor = Color.Green;
                    lblHALL2tør.BackColor = Color.Yellow;
                    ProgressionBar(parameters, 500);
                    lblHALL2tør.BackColor = Color.Green;
                    if (parameters.Vasktype == 1)
                    {
                        lblHALL2voks.BackColor = Color.Yellow;
                        ProgressionBar(parameters, 500);
                        lblHALL2voks.BackColor = Color.Green;
                    }
                    lblHALL2status.BackColor = Color.Yellow;
                    ctx.Send(status => lblHALL2status.Text = "GØR KLAR", null);
                    ProgressionBar(parameters, 500);
                    lblHALL2iblø.BackColor = Color.Gray;
                    lblHALL2vask.BackColor = Color.Gray;
                    lblHALL2uvask.BackColor = Color.Gray;
                    lblHALL2skyl.BackColor = Color.Gray;
                    lblHALL2tør.BackColor = Color.Gray;
                    lblHALL2voks.BackColor = Color.Gray;
                    ProgressionBar(parameters, 500);
                    ctx.Send(status => lblHALL2status.Text = "KLAR", null);
                    lblHALL2status.BackColor = Color.Green;
                    IgangHall2 = false;
                }
                else if (parameters.Vasktype == 0 && !IgangHall2)
                {
                    ctx.Send(status => cmbBoxHaller.SelectedIndex = 0, null);
                    lblErrorMsg.BackColor = Color.Red;
                    ctx.Send(status => lblErrorMsg.Text = "Du skal vælge en Vasktype", null);
                    Thread.Sleep(5000);
                    lblErrorMsg.BackColor = Color.Transparent;
                    ctx.Send(status => lblErrorMsg.Text = "", null);
                }
                else
                {
                    lblErrorMsg.BackColor = Color.Red;
                    ctx.Send(status => lblErrorMsg.Text = "Hall 2 er allerede igang", null);
                    Thread.Sleep(5000);
                    lblErrorMsg.BackColor = Color.Transparent;
                    ctx.Send(status => lblErrorMsg.Text = "", null);
                }
                ctx.Send(status => btnHall2Cancel.Visible = false, null);
                ctx.Send(status => pgbHall2.Value = 0, null);
            }
            catch (Exception ex)
            {
                Task t = Task.Run(() => CancelationEvent(2, ex.Message));
            }
        }
        void Hall3Vask(object data)
        {
            try
            {
                Parameters parameters = (Parameters)data;
                parameters.HallNr = 3;
                ctx.Send(status => cmbBoxVasktype.SelectedIndex = 0, null);
                parameters.ct.ThrowIfCancellationRequested();
                if (!IgangHall3)
                {
                    IgangHall3 = true;
                    ctx.Send(status => btnHall3Cancel.Visible = true, null);
                    ctx.Send(status => cmbBoxHaller.SelectedIndex = 0, null);
                    ctx.Send(status => lblHALL3status.Text = "I GANG", null);
                    lblHALL3status.BackColor = Color.Orange;
                    lblHALL3iblø.BackColor = Color.Yellow;
                    lblHALL3vask.BackColor = Color.Orange;
                    if (parameters.Vasktype == 1 || parameters.Vasktype == 2)
                    {
                        lblHALL3uvask.BackColor = Color.Orange;
                    }
                    lblHALL3skyl.BackColor = Color.Orange;
                    lblHALL3tør.BackColor = Color.Orange;
                    if (parameters.Vasktype == 1)
                    {
                        lblHALL3voks.BackColor = Color.Orange;
                    }
                    ProgressionBar(parameters, 500);
                    lblHALL3iblø.BackColor = Color.Green;
                    lblHALL3vask.BackColor = Color.Yellow;
                    ProgressionBar(parameters, 500);
                    lblHALL3vask.BackColor = Color.Green;
                    if (parameters.Vasktype == 1 || parameters.Vasktype == 2)
                    {
                        lblHALL3uvask.BackColor = Color.Yellow;
                        ProgressionBar(parameters, 500);
                        lblHALL3uvask.BackColor = Color.Green;
                    }
                    lblHALL3skyl.BackColor = Color.Yellow;
                    ProgressionBar(parameters, 500);
                    lblHALL3skyl.BackColor = Color.Green;
                    lblHALL3tør.BackColor = Color.Yellow;
                    ProgressionBar(parameters, 500);
                    lblHALL3tør.BackColor = Color.Green;
                    if (parameters.Vasktype == 1)
                    {
                        lblHALL3voks.BackColor = Color.Yellow;
                        ProgressionBar(parameters, 500);
                        lblHALL3voks.BackColor = Color.Green;
                    }
                    lblHALL3status.BackColor = Color.Yellow;
                    ctx.Send(status => lblHALL3status.Text = "GØR KLAR", null);
                    ProgressionBar(parameters, 500);
                    lblHALL3iblø.BackColor = Color.Gray;
                    lblHALL3vask.BackColor = Color.Gray;
                    lblHALL3uvask.BackColor = Color.Gray;
                    lblHALL3skyl.BackColor = Color.Gray;
                    lblHALL3tør.BackColor = Color.Gray;
                    lblHALL3voks.BackColor = Color.Gray;
                    ProgressionBar(parameters, 500);
                    ctx.Send(status => lblHALL3status.Text = "KLAR", null);
                    lblHALL3status.BackColor = Color.Green;
                    IgangHall3 = false;
                }
                else if (parameters.Vasktype == 0 && !IgangHall3)
                {
                    ctx.Send(status => cmbBoxHaller.SelectedIndex = 0, null);
                    lblErrorMsg.BackColor = Color.Red;
                    ctx.Send(status => lblErrorMsg.Text = "Du skal vælge en Vasktype", null);
                    Thread.Sleep(5000);
                    lblErrorMsg.BackColor = Color.Transparent;
                    ctx.Send(status => lblErrorMsg.Text = "", null);
                }
                else
                {
                    lblErrorMsg.BackColor = Color.Red;
                    ctx.Send(status => lblErrorMsg.Text = "Hall 3 er allerede igang", null);
                    Thread.Sleep(5000);
                    lblErrorMsg.BackColor = Color.Transparent;
                    ctx.Send(status => lblErrorMsg.Text = "", null);
                }
                ctx.Send(status => btnHall3Cancel.Visible = false, null);
                ctx.Send(status => pgbHall3.Value = 0, null);
            }
            catch (Exception ex)
            {
                Task t = Task.Run(() => CancelationEvent(3, ex.Message));
            }
        }
        async void Hall4Vask(object data)
        {
            try
            {
                Parameters parameters = (Parameters)data;
                parameters.HallNr = 4;
                ctx.Send(status => cmbBoxVasktype.SelectedIndex = 0, null);
                parameters.ct.ThrowIfCancellationRequested();
                if (!IgangHall4)
                {
                    IgangHall4 = true;
                    ctx.Send(status => btnHall4Cancel.Visible = true, null);
                    ctx.Send(status => cmbBoxHaller.SelectedIndex = 0, null);
                    ctx.Send(status => lblHALL4status.Text = "I GANG", null);
                    lblHALL4status.BackColor = Color.Orange;
                    lblHALL4iblø.BackColor = Color.Yellow;
                    lblHALL4vask.BackColor = Color.Orange;
                    if (parameters.Vasktype == 1 || parameters.Vasktype == 2)
                    {
                        lblHALL4uvask.BackColor = Color.Orange;
                    }
                    lblHALL4skyl.BackColor = Color.Orange;
                    lblHALL4tør.BackColor = Color.Orange;
                    if (parameters.Vasktype == 1)
                    {
                        lblHALL4voks.BackColor = Color.Orange;
                    }
                    ProgressionBar(parameters, 500);
                    lblHALL4iblø.BackColor = Color.Green;
                    lblHALL4vask.BackColor = Color.Yellow;
                    ProgressionBar(parameters, 500);
                    lblHALL4vask.BackColor = Color.Green;
                    if (parameters.Vasktype == 1 || parameters.Vasktype == 2)
                    {
                        lblHALL4uvask.BackColor = Color.Yellow;
                        ProgressionBar(parameters, 500);
                        lblHALL4uvask.BackColor = Color.Green;
                    }
                    lblHALL4skyl.BackColor = Color.Yellow;
                    ProgressionBar(parameters, 500);
                    lblHALL4skyl.BackColor = Color.Green;
                    lblHALL4tør.BackColor = Color.Yellow;
                    ProgressionBar(parameters, 500);
                    lblHALL4tør.BackColor = Color.Green;
                    if (parameters.Vasktype == 1)
                    {
                        lblHALL4voks.BackColor = Color.Yellow;
                        ProgressionBar(parameters, 500);
                        lblHALL4voks.BackColor = Color.Green;
                    }
                    lblHALL4status.BackColor = Color.Yellow;
                    ctx.Send(status => lblHALL4status.Text = "GØR KLAR", null);
                    ProgressionBar(parameters, 500);
                    lblHALL4iblø.BackColor = Color.Gray;
                    lblHALL4vask.BackColor = Color.Gray;
                    lblHALL4uvask.BackColor = Color.Gray;
                    lblHALL4skyl.BackColor = Color.Gray;
                    lblHALL4tør.BackColor = Color.Gray;
                    lblHALL4voks.BackColor = Color.Gray;
                    ProgressionBar(parameters, 500);
                    ctx.Send(status => lblHALL4status.Text = "KLAR", null);
                    lblHALL4status.BackColor = Color.Green;
                    IgangHall4 = false;
                }
                else if (parameters.Vasktype == 0 && !IgangHall4)
                {
                    parameters.ct.ThrowIfCancellationRequested();
                    ctx.Send(status => cmbBoxHaller.SelectedIndex = 0, null);
                    lblErrorMsg.BackColor = Color.Red;
                    ctx.Send(status => lblErrorMsg.Text = "Du skal vælge en Vasktype", null);
                    Thread.Sleep(5000);
                    lblErrorMsg.BackColor = Color.Transparent;
                    ctx.Send(status => lblErrorMsg.Text = "", null);
                }
                else
                {
                    lblErrorMsg.BackColor = Color.Red;
                    ctx.Send(status => lblErrorMsg.Text = "Hall 4 er allerede igang", null);
                    Thread.Sleep(5000);
                    lblErrorMsg.BackColor = Color.Transparent;
                    ctx.Send(status => lblErrorMsg.Text = "", null);
                }
                ctx.Send(status => btnHall4Cancel.Visible = false, null);
                ctx.Send(status => pgbHall4.Value = 0, null);
            }
            catch (Exception ex)
            {
                Task t = Task.Run(() => CancelationEvent(4, ex.Message));
            }
        }
        #endregion

        #region Progression Bar
        //Method to Control the Progression bar and check for Cancelation
        void ProgressionBar(Parameters parameters,int sleepTime)
        {
            if(parameters.HallNr == 1)
            {
                for (int i = parameters.StartNumber; i <= parameters.EndNumber; i++)
                {
                    ctx.Send(status => pgbHall1.Value = i, null);
                    Thread.Sleep(sleepTime);
                    parameters.ct.ThrowIfCancellationRequested();
                }
            }
            else if (parameters.HallNr == 2)
            {
                for (int i = parameters.StartNumber; i <= parameters.EndNumber; i++)
                {
                    ctx.Send(status => pgbHall2.Value = i, null);
                    Thread.Sleep(sleepTime);
                    parameters.ct.ThrowIfCancellationRequested();
                }
            }
            else if (parameters.HallNr == 3)
            {
                for (int i = parameters.StartNumber; i <= parameters.EndNumber; i++)
                {
                    ctx.Send(status => pgbHall3.Value = i, null);
                    Thread.Sleep(sleepTime);
                    parameters.ct.ThrowIfCancellationRequested();
                }
            }
            else if (parameters.HallNr == 4)
            {
                for (int i = parameters.StartNumber; i <= parameters.EndNumber; i++)
                {
                    ctx.Send(status => pgbHall4.Value = i, null);
                    Thread.Sleep(sleepTime);
                    parameters.ct.ThrowIfCancellationRequested();
                }
            }
        }
        #endregion

        #region Cancelation Buttons
        //Cancelation Buttons That triggers the Cancelation "Event" and sends data to it
        private void btnCancelHall1_Click(object sender, EventArgs e)
        {
            lblHALL1status.Text = "CANCELLING";
            lblHALL1status.BackColor = Color.Red;
            tcsHall1.Cancel();
        }
        private void btnCancelHall2_Click(object sender, EventArgs e)
        {
            lblHALL2status.Text = "CANCELLING";
            lblHALL2status.BackColor = Color.Red;
            tcsHall2.Cancel();
        }
        private void btnCancelHall3_Click(object sender, EventArgs e)
        {
            lblHALL3status.Text = "CANCELLING";
            lblHALL3status.BackColor = Color.Red;
            tcsHall3.Cancel();
        }
        private void btnCancelHall4_Click(object sender, EventArgs e)
        {
            lblHALL4status.Text = "CANCELLING";
            lblHALL4status.BackColor = Color.Red;
            tcsHall4.Cancel();
        }
        #endregion

        #region Cancelation Event
        // Gets called when the cancelation button is clicked sets label texts and colors according to progression of the cancelation
        void CancelationEvent(int Hallnr, string Msg)
        {
            switch (Hallnr)
            {
                case 1:
                    ctx.Send(status => lblErrorMsg.Visible = true, null);
                    lblErrorMsg.BackColor = Color.Red;
                    ctx.Send(status => lblErrorMsg.Text = "Hall 1 blev stopped! Begrundelse: " + Msg, null);
                    lblHALL1iblø.BackColor = Color.Red;
                    Thread.Sleep(500);
                    lblHALL1vask.BackColor = Color.Red;
                    Thread.Sleep(500);
                    lblHALL1uvask.BackColor = Color.Red;
                    Thread.Sleep(500);
                    lblHALL1skyl.BackColor = Color.Red;
                    Thread.Sleep(500);
                    lblHALL1tør.BackColor = Color.Red;
                    Thread.Sleep(500);
                    lblHALL1voks.BackColor = Color.Red;
                    Thread.Sleep(1000);
                    ctx.Send(status => lblHALL1status.Text = "CANCELLED", null);
                    Thread.Sleep(2000);
                    lblHALL1status.BackColor = Color.Orange;
                    ctx.Send(status => lblHALL1status.Text = "GØR KLAR", null);
                    lblHALL1iblø.BackColor = Color.Gray;
                    Thread.Sleep(500);
                    lblHALL1vask.BackColor = Color.Gray;
                    Thread.Sleep(500);
                    lblHALL1uvask.BackColor = Color.Gray;
                    Thread.Sleep(500);
                    lblHALL1skyl.BackColor = Color.Gray;
                    Thread.Sleep(500);
                    lblHALL1tør.BackColor = Color.Gray;
                    Thread.Sleep(500);
                    lblHALL1voks.BackColor = Color.Gray;
                    Thread.Sleep(1000);
                    ctx.Send(status => lblHALL1status.Text = "KLAR", null);
                    lblHALL1status.BackColor = Color.Green;
                    IgangHall1 = false;
                    ctx.Send(status => lblErrorMsg.Visible = false, null);
                    ctx.Send(status => btnHall1Cancel.Visible = false, null);
                    ctx.Send(status => pgbHall1.Value = 0, null);
                    break;
                case 2:
                    ctx.Send(status => lblErrorMsg.Visible = true, null);
                    lblErrorMsg.BackColor = Color.Red;
                    ctx.Send(status => lblErrorMsg.Text = "Hall 2 blev stopped! Begrundelse: " + Msg, null);
                    lblHALL2iblø.BackColor = Color.Red;
                    Thread.Sleep(500);
                    lblHALL2vask.BackColor = Color.Red;
                    Thread.Sleep(500);
                    lblHALL2uvask.BackColor = Color.Red;
                    Thread.Sleep(500);
                    lblHALL2skyl.BackColor = Color.Red;
                    Thread.Sleep(500);
                    lblHALL2tør.BackColor = Color.Red;
                    Thread.Sleep(500);
                    lblHALL2voks.BackColor = Color.Red;
                    Thread.Sleep(1000);
                    ctx.Send(status => lblHALL2status.Text = "CANCELLED", null);
                    Thread.Sleep(2000);
                    lblHALL2status.BackColor = Color.Orange;
                    ctx.Send(status => lblHALL2status.Text = "GØR KLAR", null);
                    lblHALL2iblø.BackColor = Color.Gray;
                    Thread.Sleep(500);
                    lblHALL2vask.BackColor = Color.Gray;
                    Thread.Sleep(500);
                    lblHALL2uvask.BackColor = Color.Gray;
                    Thread.Sleep(500);
                    lblHALL2skyl.BackColor = Color.Gray;
                    Thread.Sleep(500);
                    lblHALL2tør.BackColor = Color.Gray;
                    Thread.Sleep(500);
                    lblHALL2voks.BackColor = Color.Gray;
                    Thread.Sleep(1000);
                    ctx.Send(status => lblHALL2status.Text = "KLAR", null);
                    lblHALL2status.BackColor = Color.Green;
                    IgangHall2 = false;
                    ctx.Send(status => lblErrorMsg.Visible = false, null);
                    ctx.Send(status => btnHall2Cancel.Visible = false, null);
                    ctx.Send(status => pgbHall2.Value = 0, null);
                    break;
                case 3:
                    ctx.Send(status => lblErrorMsg.Visible = true, null);
                    lblErrorMsg.BackColor = Color.Red;
                    ctx.Send(status => lblErrorMsg.Text = "Hall 3 blev stopped! Begrundelse: " + Msg, null);
                    lblHALL3iblø.BackColor = Color.Red;
                    Thread.Sleep(500);
                    lblHALL3vask.BackColor = Color.Red;
                    Thread.Sleep(500);
                    lblHALL3uvask.BackColor = Color.Red;
                    Thread.Sleep(500);
                    lblHALL3skyl.BackColor = Color.Red;
                    Thread.Sleep(500);
                    lblHALL3tør.BackColor = Color.Red;
                    Thread.Sleep(500);
                    lblHALL3voks.BackColor = Color.Red;
                    Thread.Sleep(1000);
                    ctx.Send(status => lblHALL3status.Text = "CANCELLED", null);
                    Thread.Sleep(2000);
                    lblHALL3status.BackColor = Color.Orange;
                    ctx.Send(status => lblHALL3status.Text = "GØR KLAR", null);
                    lblHALL3iblø.BackColor = Color.Gray;
                    Thread.Sleep(500);
                    lblHALL3vask.BackColor = Color.Gray;
                    Thread.Sleep(500);
                    lblHALL3uvask.BackColor = Color.Gray;
                    Thread.Sleep(500);
                    lblHALL3skyl.BackColor = Color.Gray;
                    Thread.Sleep(500);
                    lblHALL3tør.BackColor = Color.Gray;
                    Thread.Sleep(500);
                    lblHALL3voks.BackColor = Color.Gray;
                    Thread.Sleep(1000);
                    ctx.Send(status => lblHALL3status.Text = "KLAR", null);
                    lblHALL3status.BackColor = Color.Green;
                    IgangHall3 = false;
                    ctx.Send(status => lblErrorMsg.Visible = false, null);
                    ctx.Send(status => btnHall3Cancel.Visible = false, null);
                    ctx.Send(status => pgbHall3.Value = 0, null);
                    break;
                case 4:
                    ctx.Send(status => lblErrorMsg.Visible = true, null);
                    lblErrorMsg.BackColor = Color.Red;
                    ctx.Send(status => lblErrorMsg.Text = "Hall 4 blev stopped! Begrundelse: " + Msg, null);
                    lblHALL4iblø.BackColor = Color.Red;
                    Thread.Sleep(500);
                    lblHALL4vask.BackColor = Color.Red;
                    Thread.Sleep(500);
                    lblHALL4uvask.BackColor = Color.Red;
                    Thread.Sleep(500);
                    lblHALL4skyl.BackColor = Color.Red;
                    Thread.Sleep(500);
                    lblHALL4tør.BackColor = Color.Red;
                    Thread.Sleep(500);
                    lblHALL4voks.BackColor = Color.Red;
                    Thread.Sleep(1000);
                    ctx.Send(status => lblHALL4status.Text = "CANCELLED", null);
                    Thread.Sleep(2000);
                    lblHALL4status.BackColor = Color.Orange;
                    ctx.Send(status => lblHALL4status.Text = "GØR KLAR", null);
                    lblHALL4iblø.BackColor = Color.Gray;
                    Thread.Sleep(500);
                    lblHALL4vask.BackColor = Color.Gray;
                    Thread.Sleep(500);
                    lblHALL4uvask.BackColor = Color.Gray;
                    Thread.Sleep(500);
                    lblHALL4skyl.BackColor = Color.Gray;
                    Thread.Sleep(500);
                    lblHALL4tør.BackColor = Color.Gray;
                    Thread.Sleep(500);
                    lblHALL4voks.BackColor = Color.Gray;
                    Thread.Sleep(1000);
                    ctx.Send(status => lblHALL4status.Text = "KLAR", null);
                    lblHALL4status.BackColor = Color.Green;
                    IgangHall4 = false;
                    ctx.Send(status => lblErrorMsg.Visible = false, null);
                    ctx.Send(status => btnHall4Cancel.Visible = false, null);
                    ctx.Send(status => pgbHall4.Value = 0, null);
                    break;
            }
        }
        #endregion
    }
    class Parameters
    {
        public int Vasktype { get; set; }
        public int HallNr { get; set; }
        public CancellationToken ct { get; set; }
        public int StartNumber = 0;
        public int EndNumber = 10;
    }
}
