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
        SynchronizationContext ctx = null;
        bool IgangHall1 = false;
        bool IgangHall2 = false;
        bool IgangHall3 = false;
        bool IgangHall4 = false;
        public Form2()
        {
            InitializeComponent();
        }

        private async void btnStart_Click(object sender, EventArgs e)
        {
            ctx = SynchronizationContext.Current;
            Parameters param = new Parameters();
            param.Vasktype = cmbBoxVasktype.SelectedIndex;
            if (cmbBoxHaller.SelectedIndex == 1)
            {
                ThreadPool.QueueUserWorkItem(Hall1Vask, param);
            }
            else if (cmbBoxHaller.SelectedIndex == 2)
            {
                Task t = Task.Factory.StartNew(Hall2Vask, param);
            }
            else if (cmbBoxHaller.SelectedIndex == 3)
            {
                Task t = Task.Run(() => Hall3Vask(param));
            }
            else if (cmbBoxHaller.SelectedIndex == 4)
            {
                await Task.Run(() => Hall4Vask(param));
            }
            else if (cmbBoxHaller.SelectedIndex == 0)
            {
                MessageBox.Show("Du skal vælge en vaskehall!", "Fejl");
            }
        }

        void Hall1Vask(Object data)
        {
            Parameters parameters = (Parameters)data;
            ctx.Send(status => cmbBoxVasktype.SelectedIndex = 0, null);
            if (parameters.Vasktype == 1 && !IgangHall1)
            {
                IgangHall1 = true;
                ctx.Send(status => cmbBoxHaller.SelectedIndex = 0, null);
                ctx.Send(status => lblHALL1status.Text = "I GANG", null);
                lblHALL1status.BackColor = Color.Red;
                lblHALL1iblø.BackColor = Color.Orange;
                lblHALL1vask.BackColor = Color.Red;
                lblHALL1uvask.BackColor = Color.Red;
                lblHALL1skyl.BackColor = Color.Red;
                lblHALL1tør.BackColor = Color.Red;
                lblHALL1voks.BackColor = Color.Red;
                Thread.Sleep(10000);
                lblHALL1iblø.BackColor = Color.Green;
                lblHALL1vask.BackColor = Color.Orange;
                Thread.Sleep(8000);
                lblHALL1vask.BackColor = Color.Green;
                lblHALL1uvask.BackColor = Color.Orange;
                Thread.Sleep(7000);
                lblHALL1uvask.BackColor = Color.Green;
                lblHALL1skyl.BackColor = Color.Orange;
                Thread.Sleep(5000);
                lblHALL1skyl.BackColor = Color.Green;
                lblHALL1tør.BackColor = Color.Orange;
                Thread.Sleep(10000);
                lblHALL1tør.BackColor = Color.Green;
                lblHALL1voks.BackColor = Color.Orange;
                Thread.Sleep(7000);
                lblHALL1voks.BackColor = Color.Green;
                lblHALL1status.BackColor = Color.Orange;
                ctx.Send(status => lblHALL1status.Text = "GØR KLAR", null);
                Thread.Sleep(8000);
                lblHALL1iblø.BackColor = Color.Gray;
                lblHALL1vask.BackColor = Color.Gray;
                lblHALL1uvask.BackColor = Color.Gray;
                lblHALL1skyl.BackColor = Color.Gray;
                lblHALL1tør.BackColor = Color.Gray;
                lblHALL1voks.BackColor = Color.Gray;
                Thread.Sleep(3000);
                ctx.Send(status => lblHALL1status.Text = "KLAR", null);
                lblHALL1status.BackColor = Color.Green;
                IgangHall1 = false;
            }
            else if (parameters.Vasktype == 2 && !IgangHall1)
            {
                IgangHall1 = true;
                ctx.Send(status => cmbBoxHaller.SelectedIndex = 0, null);
                ctx.Send(status => lblHALL1status.Text = "I GANG", null);
                lblHALL1status.BackColor = Color.Red;
                lblHALL1iblø.BackColor = Color.Orange;
                lblHALL1vask.BackColor = Color.Red;
                lblHALL1uvask.BackColor = Color.Red;
                lblHALL1skyl.BackColor = Color.Red;
                lblHALL1tør.BackColor = Color.Red;
                Thread.Sleep(10000);
                lblHALL1iblø.BackColor = Color.Green;
                lblHALL1vask.BackColor = Color.Orange;
                Thread.Sleep(8000);
                lblHALL1vask.BackColor = Color.Green;
                lblHALL1uvask.BackColor = Color.Orange;
                Thread.Sleep(7000);
                lblHALL1uvask.BackColor = Color.Green;
                lblHALL1skyl.BackColor = Color.Orange;
                Thread.Sleep(5000);
                lblHALL1skyl.BackColor = Color.Green;
                lblHALL1tør.BackColor = Color.Orange;
                Thread.Sleep(10000);
                lblHALL1tør.BackColor = Color.Green;
                lblHALL1status.BackColor = Color.Orange;
                ctx.Send(status => lblHALL1status.Text = "GØR KLAR", null);
                Thread.Sleep(8000);
                lblHALL1iblø.BackColor = Color.Gray;
                lblHALL1vask.BackColor = Color.Gray;
                lblHALL1uvask.BackColor = Color.Gray;
                lblHALL1skyl.BackColor = Color.Gray;
                lblHALL1tør.BackColor = Color.Gray;
                Thread.Sleep(3000);
                ctx.Send(status => lblHALL1status.Text = "KLAR", null);
                lblHALL1status.BackColor = Color.Green;
                IgangHall1 = false;
            }
            else if (parameters.Vasktype == 3 && !IgangHall1)
            {
                IgangHall1 = true;
                ctx.Send(status => cmbBoxHaller.SelectedIndex = 0, null);
                ctx.Send(status => lblHALL1status.Text = "I GANG", null);
                lblHALL1status.BackColor = Color.Red;
                lblHALL1iblø.BackColor = Color.Orange;
                lblHALL1vask.BackColor = Color.Red;
                lblHALL1skyl.BackColor = Color.Red;
                lblHALL1tør.BackColor = Color.Red;
                Thread.Sleep(10000);
                lblHALL1iblø.BackColor = Color.Green;
                lblHALL1vask.BackColor = Color.Orange;
                Thread.Sleep(8000);
                lblHALL1vask.BackColor = Color.Green;
                lblHALL1skyl.BackColor = Color.Orange;
                Thread.Sleep(5000);
                lblHALL1skyl.BackColor = Color.Green;
                lblHALL1tør.BackColor = Color.Orange;
                Thread.Sleep(10000);
                lblHALL1tør.BackColor = Color.Green;
                lblHALL1status.BackColor = Color.Orange;
                ctx.Send(status => lblHALL1status.Text = "GØR KLAR", null);
                Thread.Sleep(8000);
                lblHALL1iblø.BackColor = Color.Gray;
                lblHALL1vask.BackColor = Color.Gray;
                lblHALL1uvask.BackColor = Color.Gray;
                lblHALL1skyl.BackColor = Color.Gray;
                lblHALL1tør.BackColor = Color.Gray;
                Thread.Sleep(3000);
                ctx.Send(status => lblHALL1status.Text = "KLAR", null);
                lblHALL1status.BackColor = Color.Green;
                IgangHall1 = false;
            }
            else if (parameters.Vasktype == 0 && !IgangHall1)
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
                ctx.Send(status => lblErrorMsg.Text = "Hall 1 er allerede igang", null);
                Thread.Sleep(5000);
                lblErrorMsg.BackColor = Color.Transparent;
                ctx.Send(status => lblErrorMsg.Text = "", null);
            }
        }
        void Hall2Vask(object data)
        {
            Parameters parameters = (Parameters)data;
            ctx.Send(status => cmbBoxVasktype.SelectedIndex = 0, null);
            if (parameters.Vasktype == 1 && !IgangHall2)
            {
                IgangHall2 = true;
                ctx.Send(status => cmbBoxHaller.SelectedIndex = 0, null);
                ctx.Send(status => lblHALL2status.Text = "I GANG", null);
                lblHALL2status.BackColor = Color.Red;
                lblHALL2iblø.BackColor = Color.Orange;
                lblHALL2vask.BackColor = Color.Red;
                lblHALL2uvask.BackColor = Color.Red;
                lblHALL2skyl.BackColor = Color.Red;
                lblHALL2tør.BackColor = Color.Red;
                lblHALL2voks.BackColor = Color.Red;
                Thread.Sleep(10000);
                lblHALL2iblø.BackColor = Color.Green;
                lblHALL2vask.BackColor = Color.Orange;
                Thread.Sleep(8000);
                lblHALL2vask.BackColor = Color.Green;
                lblHALL2uvask.BackColor = Color.Orange;
                Thread.Sleep(7000);
                lblHALL2uvask.BackColor = Color.Green;
                lblHALL2skyl.BackColor = Color.Orange;
                Thread.Sleep(5000);
                lblHALL2skyl.BackColor = Color.Green;
                lblHALL2tør.BackColor = Color.Orange;
                Thread.Sleep(10000);
                lblHALL2tør.BackColor = Color.Green;
                lblHALL2voks.BackColor = Color.Orange;
                Thread.Sleep(7000);
                lblHALL2voks.BackColor = Color.Green;
                lblHALL2status.BackColor = Color.Orange;
                ctx.Send(status => lblHALL2status.Text = "GØR KLAR", null);
                Thread.Sleep(8000);
                lblHALL2iblø.BackColor = Color.Gray;
                lblHALL2vask.BackColor = Color.Gray;
                lblHALL2uvask.BackColor = Color.Gray;
                lblHALL2skyl.BackColor = Color.Gray;
                lblHALL2tør.BackColor = Color.Gray;
                lblHALL2voks.BackColor = Color.Gray;
                Thread.Sleep(3000);
                ctx.Send(status => lblHALL2status.Text = "KLAR", null);
                lblHALL2status.BackColor = Color.Green;
                IgangHall2 = false;
            }
            else if (parameters.Vasktype == 2 && !IgangHall2)
            {
                IgangHall2 = true;
                ctx.Send(status => cmbBoxHaller.SelectedIndex = 0, null);
                ctx.Send(status => lblHALL2status.Text = "I GANG", null);
                lblHALL2status.BackColor = Color.Red;
                lblHALL2iblø.BackColor = Color.Orange;
                lblHALL2vask.BackColor = Color.Red;
                lblHALL2uvask.BackColor = Color.Red;
                lblHALL2skyl.BackColor = Color.Red;
                lblHALL2tør.BackColor = Color.Red;
                Thread.Sleep(10000);
                lblHALL2iblø.BackColor = Color.Green;
                lblHALL2vask.BackColor = Color.Orange;
                Thread.Sleep(8000);
                lblHALL2vask.BackColor = Color.Green;
                lblHALL2uvask.BackColor = Color.Orange;
                Thread.Sleep(7000);
                lblHALL2uvask.BackColor = Color.Green;
                lblHALL2skyl.BackColor = Color.Orange;
                Thread.Sleep(5000);
                lblHALL2skyl.BackColor = Color.Green;
                lblHALL2tør.BackColor = Color.Orange;
                Thread.Sleep(10000);
                lblHALL2tør.BackColor = Color.Green;
                lblHALL2status.BackColor = Color.Orange;
                ctx.Send(status => lblHALL2status.Text = "GØR KLAR", null);
                Thread.Sleep(8000);
                lblHALL2iblø.BackColor = Color.Gray;
                lblHALL2vask.BackColor = Color.Gray;
                lblHALL2uvask.BackColor = Color.Gray;
                lblHALL2skyl.BackColor = Color.Gray;
                lblHALL2tør.BackColor = Color.Gray;
                Thread.Sleep(3000);
                ctx.Send(status => lblHALL2status.Text = "KLAR", null);
                lblHALL2status.BackColor = Color.Green;
                IgangHall2 = false;
            }
            else if (parameters.Vasktype == 3 && !IgangHall2)
            {
                IgangHall2 = true;
                ctx.Send(status => cmbBoxHaller.SelectedIndex = 0, null);
                ctx.Send(status => lblHALL2status.Text = "I GANG", null);
                lblHALL2status.BackColor = Color.Red;
                lblHALL2iblø.BackColor = Color.Orange;
                lblHALL2vask.BackColor = Color.Red;
                lblHALL2skyl.BackColor = Color.Red;
                lblHALL2tør.BackColor = Color.Red;
                Thread.Sleep(10000);
                lblHALL2iblø.BackColor = Color.Green;
                lblHALL2vask.BackColor = Color.Orange;
                Thread.Sleep(8000);
                lblHALL2vask.BackColor = Color.Green;
                lblHALL2skyl.BackColor = Color.Orange;
                Thread.Sleep(5000);
                lblHALL2skyl.BackColor = Color.Green;
                lblHALL2tør.BackColor = Color.Orange;
                Thread.Sleep(10000);
                lblHALL2tør.BackColor = Color.Green;
                lblHALL2status.BackColor = Color.Orange;
                ctx.Send(status => lblHALL2status.Text = "GØR KLAR", null);
                Thread.Sleep(8000);
                lblHALL2iblø.BackColor = Color.Gray;
                lblHALL2vask.BackColor = Color.Gray;
                lblHALL2uvask.BackColor = Color.Gray;
                lblHALL2skyl.BackColor = Color.Gray;
                lblHALL2tør.BackColor = Color.Gray;
                Thread.Sleep(3000);
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
        }
        void Hall3Vask(object data)
        {
            Parameters parameters = (Parameters)data;
            ctx.Send(status => cmbBoxVasktype.SelectedIndex = 0, null);
            if (parameters.Vasktype == 1 && !IgangHall3)
            {
                IgangHall3 = true;
                ctx.Send(status => cmbBoxHaller.SelectedIndex = 0, null);
                ctx.Send(status => lblHALL3status.Text = "I GANG", null);
                lblHALL3status.BackColor = Color.Red;
                lblHALL3iblø.BackColor = Color.Orange;
                lblHALL3vask.BackColor = Color.Red;
                lblHALL3uvask.BackColor = Color.Red;
                lblHALL3skyl.BackColor = Color.Red;
                lblHALL3tør.BackColor = Color.Red;
                lblHALL3voks.BackColor = Color.Red;
                Thread.Sleep(10000);
                lblHALL3iblø.BackColor = Color.Green;
                lblHALL3vask.BackColor = Color.Orange;
                Thread.Sleep(8000);
                lblHALL3vask.BackColor = Color.Green;
                lblHALL3uvask.BackColor = Color.Orange;
                Thread.Sleep(7000);
                lblHALL3uvask.BackColor = Color.Green;
                lblHALL3skyl.BackColor = Color.Orange;
                Thread.Sleep(5000);
                lblHALL3skyl.BackColor = Color.Green;
                lblHALL3tør.BackColor = Color.Orange;
                Thread.Sleep(10000);
                lblHALL3tør.BackColor = Color.Green;
                lblHALL3voks.BackColor = Color.Orange;
                Thread.Sleep(7000);
                lblHALL3voks.BackColor = Color.Green;
                lblHALL3status.BackColor = Color.Orange;
                ctx.Send(status => lblHALL3status.Text = "GØR KLAR", null);
                Thread.Sleep(8000);
                lblHALL3iblø.BackColor = Color.Gray;
                lblHALL3vask.BackColor = Color.Gray;
                lblHALL3uvask.BackColor = Color.Gray;
                lblHALL3skyl.BackColor = Color.Gray;
                lblHALL3tør.BackColor = Color.Gray;
                lblHALL3voks.BackColor = Color.Gray;
                Thread.Sleep(3000);
                ctx.Send(status => lblHALL3status.Text = "KLAR", null);
                lblHALL3status.BackColor = Color.Green;
                IgangHall3 = false;
            }
            else if (parameters.Vasktype == 2 && !IgangHall3)
            {
                IgangHall3 = true;
                ctx.Send(status => cmbBoxHaller.SelectedIndex = 0, null);
                ctx.Send(status => lblHALL3status.Text = "I GANG", null);
                lblHALL3status.BackColor = Color.Red;
                lblHALL3iblø.BackColor = Color.Orange;
                lblHALL3vask.BackColor = Color.Red;
                lblHALL3uvask.BackColor = Color.Red;
                lblHALL3skyl.BackColor = Color.Red;
                lblHALL3tør.BackColor = Color.Red;
                Thread.Sleep(10000);
                lblHALL3iblø.BackColor = Color.Green;
                lblHALL3vask.BackColor = Color.Orange;
                Thread.Sleep(8000);
                lblHALL3vask.BackColor = Color.Green;
                lblHALL3uvask.BackColor = Color.Orange;
                Thread.Sleep(7000);
                lblHALL3uvask.BackColor = Color.Green;
                lblHALL3skyl.BackColor = Color.Orange;
                Thread.Sleep(5000);
                lblHALL3skyl.BackColor = Color.Green;
                lblHALL3tør.BackColor = Color.Orange;
                Thread.Sleep(10000);
                lblHALL3tør.BackColor = Color.Green;
                lblHALL3status.BackColor = Color.Orange;
                ctx.Send(status => lblHALL3status.Text = "GØR KLAR", null);
                Thread.Sleep(8000);
                lblHALL3iblø.BackColor = Color.Gray;
                lblHALL3vask.BackColor = Color.Gray;
                lblHALL3uvask.BackColor = Color.Gray;
                lblHALL3skyl.BackColor = Color.Gray;
                lblHALL3tør.BackColor = Color.Gray;
                Thread.Sleep(3000);
                ctx.Send(status => lblHALL3status.Text = "KLAR", null);
                lblHALL3status.BackColor = Color.Green;
                IgangHall3 = false;
            }
            else if (parameters.Vasktype == 3 && !IgangHall3)
            {
                IgangHall3 = true;
                ctx.Send(status => cmbBoxHaller.SelectedIndex = 0, null);
                ctx.Send(status => lblHALL3status.Text = "I GANG", null);
                lblHALL3status.BackColor = Color.Red;
                lblHALL3iblø.BackColor = Color.Orange;
                lblHALL3vask.BackColor = Color.Red;
                lblHALL3skyl.BackColor = Color.Red;
                lblHALL3tør.BackColor = Color.Red;
                Thread.Sleep(10000);
                lblHALL3iblø.BackColor = Color.Green;
                lblHALL3vask.BackColor = Color.Orange;
                Thread.Sleep(8000);
                lblHALL3vask.BackColor = Color.Green;
                lblHALL3skyl.BackColor = Color.Orange;
                Thread.Sleep(5000);
                lblHALL3skyl.BackColor = Color.Green;
                lblHALL3tør.BackColor = Color.Orange;
                Thread.Sleep(10000);
                lblHALL3tør.BackColor = Color.Green;
                lblHALL3status.BackColor = Color.Orange;
                ctx.Send(status => lblHALL3status.Text = "GØR KLAR", null);
                Thread.Sleep(8000);
                lblHALL3iblø.BackColor = Color.Gray;
                lblHALL3vask.BackColor = Color.Gray;
                lblHALL3uvask.BackColor = Color.Gray;
                lblHALL3skyl.BackColor = Color.Gray;
                lblHALL3tør.BackColor = Color.Gray;
                Thread.Sleep(3000);
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
        }
        async void Hall4Vask(object data)
        {
            Parameters parameters = (Parameters)data;
            ctx.Send(status => cmbBoxVasktype.SelectedIndex = 0, null);
            if (parameters.Vasktype == 1 && !IgangHall4)
            {
                IgangHall4 = true;
                ctx.Send(status => cmbBoxHaller.SelectedIndex = 0, null);
                ctx.Send(status => lblHALL4status.Text = "I GANG", null);
                lblHALL4status.BackColor = Color.Red;
                lblHALL4iblø.BackColor = Color.Orange;
                lblHALL4vask.BackColor = Color.Red;
                lblHALL4uvask.BackColor = Color.Red;
                lblHALL4skyl.BackColor = Color.Red;
                lblHALL4tør.BackColor = Color.Red;
                lblHALL4voks.BackColor = Color.Red;
                Thread.Sleep(10000);
                lblHALL4iblø.BackColor = Color.Green;
                lblHALL4vask.BackColor = Color.Orange;
                Thread.Sleep(8000);
                lblHALL4vask.BackColor = Color.Green;
                lblHALL4uvask.BackColor = Color.Orange;
                Thread.Sleep(7000);
                lblHALL4uvask.BackColor = Color.Green;
                lblHALL4skyl.BackColor = Color.Orange;
                Thread.Sleep(5000);
                lblHALL4skyl.BackColor = Color.Green;
                lblHALL4tør.BackColor = Color.Orange;
                Thread.Sleep(10000);
                lblHALL4tør.BackColor = Color.Green;
                lblHALL4voks.BackColor = Color.Orange;
                Thread.Sleep(7000);
                lblHALL4voks.BackColor = Color.Green;
                lblHALL4status.BackColor = Color.Orange;
                ctx.Send(status => lblHALL4status.Text = "GØR KLAR", null);
                Thread.Sleep(8000);
                lblHALL4iblø.BackColor = Color.Gray;
                lblHALL4vask.BackColor = Color.Gray;
                lblHALL4uvask.BackColor = Color.Gray;
                lblHALL4skyl.BackColor = Color.Gray;
                lblHALL4tør.BackColor = Color.Gray;
                lblHALL4voks.BackColor = Color.Gray;
                Thread.Sleep(3000);
                ctx.Send(status => lblHALL4status.Text = "KLAR", null);
                lblHALL4status.BackColor = Color.Green;
                IgangHall4 = false;
            }
            else if (parameters.Vasktype == 2 && !IgangHall4)
            {
                IgangHall4 = true;
                ctx.Send(status => cmbBoxHaller.SelectedIndex = 0, null);
                ctx.Send(status => lblHALL4status.Text = "I GANG", null);
                lblHALL4status.BackColor = Color.Red;
                lblHALL4iblø.BackColor = Color.Orange;
                lblHALL4vask.BackColor = Color.Red;
                lblHALL4uvask.BackColor = Color.Red;
                lblHALL4skyl.BackColor = Color.Red;
                lblHALL4tør.BackColor = Color.Red;
                Thread.Sleep(10000);
                lblHALL4iblø.BackColor = Color.Green;
                lblHALL4vask.BackColor = Color.Orange;
                Thread.Sleep(8000);
                lblHALL4vask.BackColor = Color.Green;
                lblHALL4uvask.BackColor = Color.Orange;
                Thread.Sleep(7000);
                lblHALL4uvask.BackColor = Color.Green;
                lblHALL4skyl.BackColor = Color.Orange;
                Thread.Sleep(5000);
                lblHALL4skyl.BackColor = Color.Green;
                lblHALL4tør.BackColor = Color.Orange;
                Thread.Sleep(10000);
                lblHALL4tør.BackColor = Color.Green;
                lblHALL4status.BackColor = Color.Orange;
                ctx.Send(status => lblHALL4status.Text = "GØR KLAR", null);
                Thread.Sleep(8000);
                lblHALL4iblø.BackColor = Color.Gray;
                lblHALL4vask.BackColor = Color.Gray;
                lblHALL4uvask.BackColor = Color.Gray;
                lblHALL4skyl.BackColor = Color.Gray;
                lblHALL4tør.BackColor = Color.Gray;
                Thread.Sleep(3000);
                ctx.Send(status => lblHALL4status.Text = "KLAR", null);
                lblHALL4status.BackColor = Color.Green;
                IgangHall4 = false;
            }
            else if (parameters.Vasktype == 3 && !IgangHall4)
            {
                IgangHall4 = true;
                ctx.Send(status => cmbBoxHaller.SelectedIndex = 0, null);
                ctx.Send(status => lblHALL4status.Text = "I GANG", null);
                lblHALL4status.BackColor = Color.Red;
                lblHALL4iblø.BackColor = Color.Orange;
                lblHALL4vask.BackColor = Color.Red;
                lblHALL4skyl.BackColor = Color.Red;
                lblHALL4tør.BackColor = Color.Red;
                Thread.Sleep(10000);
                lblHALL4iblø.BackColor = Color.Green;
                lblHALL4vask.BackColor = Color.Orange;
                Thread.Sleep(8000);
                lblHALL4vask.BackColor = Color.Green;
                lblHALL4skyl.BackColor = Color.Orange;
                Thread.Sleep(5000);
                lblHALL4skyl.BackColor = Color.Green;
                lblHALL4tør.BackColor = Color.Orange;
                Thread.Sleep(10000);
                lblHALL4tør.BackColor = Color.Green;
                lblHALL4status.BackColor = Color.Orange;
                ctx.Send(status => lblHALL4status.Text = "GØR KLAR", null);
                Thread.Sleep(8000);
                lblHALL4iblø.BackColor = Color.Gray;
                lblHALL4vask.BackColor = Color.Gray;
                lblHALL4uvask.BackColor = Color.Gray;
                lblHALL4skyl.BackColor = Color.Gray;
                lblHALL4tør.BackColor = Color.Gray;
                Thread.Sleep(3000);
                ctx.Send(status => lblHALL4status.Text = "KLAR", null);
                lblHALL4status.BackColor = Color.Green;
                IgangHall4 = false;
            }
            else if (parameters.Vasktype == 0 && !IgangHall4)
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
                ctx.Send(status => lblErrorMsg.Text = "Hall 4 er allerede igang", null);
                Thread.Sleep(5000);
                lblErrorMsg.BackColor = Color.Transparent;
                ctx.Send(status => lblErrorMsg.Text = "", null);
            }
        }
    }
    class Parameters
    {
        public int Vasktype { get; set; }
    }
}
