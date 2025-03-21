using System.Windows.Forms;
using System;
using System.Data.SqlClient;
using System.Data;

namespace CRUDWindowsForm 
{
    public partial class Form1 : Form {

        string connectionString = "Data Source=KAMILIA\\KAMILIANURMALA;;Initial Catalog=DbProgramSiswa;Integrated Security=True;";

        public Form1() 
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) 
        {
            LoadData();
        }

        private void ClearForm() 
        {
            txtNIM.Clear();
            txtNama.Clear();
            txtEmail.Clear();
            txtTelepon.Clear();
            txtAlamat.Clear();

            txtNIM.Focus();
        }

        private void LoadData() 
        {
            using (SqlConnection conn = new SqlConnection(connectionString)) 
            { 
                try 
                {
                    conn.Open();
                    string query = "SELECT NIM AS [NIM], Nama, Email, Telepon, Alamat FROM Mahasiswa";
                    
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dgvMahasiswa.AutoGenerateColumns = true;
                    dgvMahasiswa.DataSource = dt;
                    ClearForm();
                }
            }
        }
    }

}