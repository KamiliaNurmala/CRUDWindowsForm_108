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
                catch (Exception ex) 
                {
                    MessageBox.Show("Error: " + ex.Message,
                    "Kesalahan",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                }
            }
        }

        private void BtnTambah(object sender, EventArgs e) 
        {
            using (SqlConnection conn = new SqlConnection(connectionString)) 
            { 
                try 
                {
                    if (txtNIM.Text == "" || txtNama.Text == "" || txtEmail.Text == "" || txtTelepon.Text == "" || txtAlamat.Text == "") 
                    {
                        MessageBox.Show("Harap isi semua data!",
                        "Peringatan",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                        return;
                    }

                    conn.Open();
                    string query = "INSERT INTO Mahasiswa (NIM, Nama, Email, Telepon, Alamat) " +
                       "VALUES (@NIM, @Nama, @Email, @Telepon, @Alamat)";

                    using (SqlCommand cmd = new SqlCommand(query, conn)) 
                    {
                        cmd.Parameters.AddWithValue("@NIM", txtNIM.Text);
                        cmd.Parameters.AddWithValue("@Nama", txtNama.Text);
                        cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                        cmd.Parameters.AddWithValue("@Telepon", txtTelepon.Text);
                        cmd.Parameters.AddWithValue("@Alamat", txtAlamat.Text);

                        int rowAffected = cmd.ExecuteNonQuery();
                        if (rowAffected > 0) {
                            MessageBox.Show("Data berhasil ditambahkan!",
                            "Sukses",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

                            LoadData();
                            ClearForm();
                        }
                        else 
                        {
                            MessageBox.Show("Data gagal ditambahkan!",
                            "Kesalahan",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex) 
                {
                    MessageBox.Show("Error: " + ex.Message,
                    "Kesalahan",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                }
            }
        }

        private void BtnHapus(object sender, EventArgs e) 
        {
            if (dgvMahasiswa.SelectedRows.Count > 0) 
            {
                DialogResult confirm = MessageBox.Show("Apakah Anda yakin ingin menghapus data ini?",
                                       "Konfirmasi",
                                       MessageBoxButtons.YesNo,
                                       MessageBoxIcon.Question);

                if (confirm == DialogResult.Yes) 
                {
                    using (SqlConnection conn = new SqlConnection(connectionString)) 
                    { 
                        try 
                        {
                            string nim = dgvMahasiswa.SelectedRows[0].Cells["NIM"].Value.ToString();
                            conn.Open();
                            string query = "DELETE FROM Mahasiswa WHERE NIM = @NIM";

                            using (SqlCommand cmd = new SqlCommand(query, conn)) 
                            {
                                cmd.Parameters.AddWithValue("@NIM", nim);
                                int rowAffected = cmd.ExecuteNonQuery();
                                if (rowAffected > 0) 
                                {
                                    MessageBox.Show("Data berhasil dihapus!",
                                    "Sukses",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);

                                    LoadData();
                                    ClearForm();
                                }
                                else 
                                {
                                    MessageBox.Show("Data tidak ditemukan atau gagal dihapus!",
                                      "Kesalahan",
                                      MessageBoxButtons.OK,
                                      MessageBoxIcon.Error);
                                }
                            }
                        }
                        catch (Exception ex) 
                        {
                            MessageBox.Show("Error: " + ex.Message,
                            "Kesalahan",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        }
                    }
                }
            }
            else 
            {
                MessageBox.Show("Pilih data yang akan dihapus!",
                "Peringatan",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            }
        }

        private void BtnRefresh(object sender, EventArgs e)
    }

}