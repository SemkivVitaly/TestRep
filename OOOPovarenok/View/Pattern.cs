﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace OOOPovarenok
{
    public partial class Pattern : Form
    {
        public Pattern()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Начальные настройки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Pattern_Load(object sender, EventArgs e)
        {
            tableLayoutPanelBottom.BackColor = Color.FromArgb(255, 204, 153);
            tableLayoutPanelTop.BackColor = Color.FromArgb(255, 204, 153);

            
        }

        /// <summary>
        /// Выход с формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonExit_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Вы действительно хотите выйти?", "Выход", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                this.Close();
            }
            else if (dialogResult == DialogResult.No)
            {
               
            }
        }
    }
}
