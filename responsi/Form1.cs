using Npgsql;
using System.Data;

namespace responsi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private NpgsqlConnection conn;

        public NpgsqlCommand cmd;
        string constr = "Server=localhost;Port=5432;Database=responsi;User Id=postgres;Password=informatika";
        private string sql = "";
        public DataTable dt;
        private DataGridViewRow row;


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            conn = new NpgsqlConnection(constr);
            RefreshData();

        }
        private void RefreshData()
        {
            try
            {
                conn.Open();
                dataGridView1.DataSource = null;
                sql = "select karyawan.id_karyawan, karyawan.nama, departemen.nama_dep, karyawan.id_dep from \"karyawan\" inner join \"departemen\" on karyawan.id_dep=departemen.id_dep";
                cmd = new NpgsqlCommand(sql, conn);
                dt = new DataTable();
                NpgsqlDataReader reader = cmd.ExecuteReader();
                dt.Load(reader);
                dataGridView1.DataSource = dt;
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.Message, "failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                conn.Open();
                sql = "INSERT INTO karyawan (nama,id_dep) values ('" + textBox1.Text + "','" + comboBox1.SelectedItem + "');";
                cmd = new NpgsqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Data user berhasil ditambahkan", "nice", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn.Close();
                RefreshData();
                textBox1.Text = comboBox1.Text = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.Message, "insert failed", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            row = dataGridView1.Rows[e.RowIndex];
            textBox1.Text = row.Cells["nama"].Value.ToString();
            comboBox1.SelectedItem = row.Cells["id_dep"].Value.ToString();
        }
    }
}