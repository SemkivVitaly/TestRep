using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Helper;
using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using OOOPovarenok.View;

namespace OOOPovarenok
{
    public partial class Autorisation : Form
    {
        public Autorisation()
        {
            InitializeComponent();
        }

        bool b;

        /// <summary>
        /// Начальные настройки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Autorisation_Load(object sender, EventArgs e)
        {
            tableLayoutPanelBottom.BackColor = Color.FromArgb(255, 204, 153);
            tableLayoutPanelTop.BackColor = Color.FromArgb(255, 204, 153);

            textBoxPassword.UseSystemPasswordChar = true;


            Helper.Helper.connectionString = "Data Source = ; Initial Catalog = ; Integrated Security = false"; //"Data Source = ; Initial Catalog = ; Persist Security Info = true; User ID = ; Password =";
            Helper.Helper.connection = new SqlConnection(Helper.Helper.connectionString);
            try 
            {
                    Helper.Helper.connection.Open();
            }
            catch(SqlException ex)
            {
                MessageBox.Show("Ошибка подключения к БД", "Ошибка подключения", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            MessageBox.Show("Подключение к БД", "Подключение к БД прошло успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        /// <summary>
        /// Авторизация пользователя
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonLogin_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Capcha().ShowDialog();
            this.Show();
            if (b == true)
              {
                SqlCommand command = Helper.Helper.connection.CreateCommand();
                string login = textBoxLogin.Text;
                string password = textBoxPassword.Text;

                if (String.IsNullOrEmpty(login) || String.IsNullOrEmpty(password))
                {
                    MessageBox.Show("Не все данные введены", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                string sql = $"Select * FROM [User] WHERE UserLogin='{login}' AND UserPassword='{password}'";
                command.CommandText = sql;
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    Helper.Helper.userID = (int)reader["UserID"];
                    Helper.Helper.userRoleID = (int)reader["UserRoleID"];
                    Helper.Helper.fullName = (string)reader["UserSurname"] + " " + (string)reader["UserName"] + " " + (string)reader["UserPatronymic"];

                    MessageBox.Show("Вы вошли с ролью " + (Role)Helper.Helper.userRoleID + "\n" + "\n" + "Вы " + Helper.Helper.fullName, "Данные о пользователе", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Вы не зарегистрированы", "Ошибка авторизации", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                reader.Close();
            }


        }

        private void buttonGuest_Click(object sender, EventArgs e)
        {
            Helper.Helper.userRoleID = 0;
            Helper.Helper.fullName = "Гость";
            MessageBox.Show("Вы вошли с ролью" + (Role)Helper.Helper.userRoleID, "Данные о пользователе", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Hide();
            new Products().ShowDialog();
            this.Show();
        }

        private void checkBoxShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxShowPassword.Checked)
            {
                textBoxPassword.UseSystemPasswordChar = false;
            }
            else
            {
                textBoxPassword.UseSystemPasswordChar = true;
            }
        }
    }
}
