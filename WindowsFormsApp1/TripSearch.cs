using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using System.Windows.Forms;
using WindowsFormsApp1.forms;
namespace WindowsFormsApp1
{
    public partial class TripSearch : Form
    {
        public TripSearch()
        {
            InitializeComponent();

        }
        List<string> titles = new List<string>();
        List<string> destinations = new List<string>();
        List<string> capacities = new List<string>();
        List<string> types = new List<string>();
        List<string> startDates = new List<string>();
        List<string> endDates = new List<string>();
        private void TripSearch_Load(object sender, EventArgs e)
        {
        string connectionString = @"Data Source=HADI-HP\SQLEXPRESS;Initial Catalog=travelease;Integrated Security=True;";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query1 = "SELECT Title, Destination, Capacity, Type, StartDate, EndDate FROM trip";
                SqlCommand cmd = new SqlCommand(query1, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    titles.Add(reader.GetString(0));
                    destinations.Add(reader.GetString(1));
                    capacities.Add(reader.GetInt32(2).ToString());
                    types.Add(reader.GetString(3));
                    startDates.Add(reader.GetDateTime(4).ToShortDateString());
                    endDates.Add(reader.GetDateTime(5).ToShortDateString());
                }
            }

            // Apply AutoComplete for each textbox
            SetAutoComplete(triptitle, titles);
            SetAutoComplete(tripdestination, destinations);
            SetAutoComplete(tripgroupsize, capacities);
            SetAutoComplete(tripactivity, types);
            SetAutoComplete(tripstart, startDates);
            SetAutoComplete(tripenddate, endDates);


            DataTable tripTable = new DataTable();
            string query = "SELECT Title, Type, Destination, Capacity, Description FROM Trip";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                adapter.Fill(tripTable);
            }
            tripdatagrid.DataSource = tripTable; // Optional: for debugging
            tripdatagrid.Visible = false;

            flowLayoutPanel1.AutoScroll = true;
            flowLayoutPanel1.WrapContents = true;
            flowLayoutPanel1.FlowDirection = FlowDirection.LeftToRight;
        }
        private void SetAutoComplete(TextBox textbox, List<string> sourceList)
        {
            AutoCompleteStringCollection autoSource = new AutoCompleteStringCollection();
            autoSource.AddRange(sourceList.ToArray());

            textbox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            textbox.AutoCompleteSource = AutoCompleteSource.CustomSource;
            textbox.AutoCompleteCustomSource = autoSource;
        }

        private void menu_Click(object sender, EventArgs e)
        {
            TripSearch f3 = new TripSearch();
            f3.Dock = DockStyle.Fill;
            f3.TopLevel = false;
            Form1.MainPanel.Controls.Clear();
            Form1.MainPanel.Controls.Add(f3);


            f3.Show(); // Add Show() to display the form
        }

        private void button1_Click(object sender, EventArgs e)
        {
            digitalTravel f3 = new digitalTravel();
            f3.Dock = DockStyle.Fill;
            f3.TopLevel = false;
            Form1.MainPanel.Controls.Clear();
            Form1.MainPanel.Controls.Add(f3);


            f3.Show(); // Add Show() to display the form
        }

        private void button3_Click(object sender, EventArgs e)
        {
            review f3 = new review();
            f3.Dock = DockStyle.Fill;
            f3.TopLevel = false;
            Form1.MainPanel.Controls.Clear();
            Form1.MainPanel.Controls.Add(f3);


            f3.Show(); // Add Show() to display the form
        }

        private void button2_Click(object sender, EventArgs e)
        {
            preferences f3 = new preferences();
            f3.Dock = DockStyle.Fill;
            f3.TopLevel = false;
            Form1.MainPanel.Controls.Clear();
            Form1.MainPanel.Controls.Add(f3);


            f3.Show(); // Add Show() to display the form
        }

        private void button5_Click(object sender, EventArgs e)
        {
            login f3 = new login();
            f3.Dock = DockStyle.Fill;
            f3.TopLevel = false;
            Form1.MainPanel.Controls.Clear();
            Form1.MainPanel.Controls.Add(f3);


            f3.Show(); // Add Show() to display the form
        }



        private void pictureBox2_Click(object sender, EventArgs e)
        {
            login f3 = new login();
            f3.Dock = DockStyle.Fill;
            f3.TopLevel = false;
            Form1.MainPanel.Controls.Clear();
            Form1.MainPanel.Controls.Add(f3);


            f3.Show(); // Add Show() to display the form
        }

        private void searchbar_TextChanged(object sender, EventArgs e)
        {


        }
        private (int BookingId, decimal TotalCost) bookTrip(string tripId)
        {
            string numberOfPeople = noOfPeopletext.Text;
            if (!int.TryParse(numberOfPeople, out int numPeople) || numPeople <= 0)
            {
                MessageBox.Show("Please enter a valid number of people.");
                return (-1, 0m); // Return -1 and 0 to indicate failure
            }

            string connectionString = @"Data Source=HADI-HP\SQLEXPRESS;Initial Catalog=travelease;Integrated Security=True;";
            int newBookingId = -1;
            decimal totalCost = 0m;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string query = @"
                DECLARE @nextId INT;
                DECLARE @tripCost DECIMAL(10,2);
                DECLARE @totalCost DECIMAL(10,2);

                SELECT @nextId = ISNULL(MAX(BookingID), 0) + 1 FROM Booking;
                SELECT @tripCost = Price FROM Trip WHERE TripID = @tripId;
                SET @totalCost = @tripCost * @numberOfPeople;

                INSERT INTO Booking (
                    BookingID, NumberOfPeople, BookingDate, timeOfPayment, timeofbooking, Cost, Status, TourOperatorID, TravellerID, TripID
                )
                OUTPUT INSERTED.BookingID, INSERTED.Cost
                VALUES (
                    @nextId, @numberOfPeople, CAST(GETDATE() AS DATE), NULL, CAST(GETDATE() AS TIME), @totalCost, 'Pending', NULL, @travellerId, @tripId
                );";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@tripId", tripId);
                    cmd.Parameters.AddWithValue("@numberOfPeople", numberOfPeople);
                    cmd.Parameters.AddWithValue("@travellerId", SessionData.id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            newBookingId = reader.GetInt32(0); // First column is BookingID
                            totalCost = reader.GetDecimal(1);  // Second column is Cost
                        }
                    }
                }

                MessageBox.Show($"Booking done. Booking ID: {newBookingId}, Total Cost: {totalCost:C}");
            }

            return (newBookingId, totalCost); // Return both BookingId and totalCost
        }


        private void payBooking(int bookingId, decimal payment)
        {
            travellerPayment paymentForm = new travellerPayment(bookingId, payment);
            paymentForm.Dock = DockStyle.Fill;
            paymentForm.TopLevel = false;
            Form1.MainPanel.Controls.Clear();
            Form1.MainPanel.Controls.Add(paymentForm);
            paymentForm.Show();
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=HADI-HP\SQLEXPRESS;Initial Catalog=travelease;Integrated Security=True;";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                StringBuilder query = new StringBuilder("SELECT * FROM trip WHERE 1=1");

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                if (!string.IsNullOrWhiteSpace(triptitle.Text))
                {
                    query.Append(" AND Title LIKE @title");
                    cmd.Parameters.AddWithValue("@title", "%" + triptitle.Text + "%");
                }

                if (!string.IsNullOrWhiteSpace(tripdestination.Text))
                {
                    query.Append(" AND Destination LIKE @destination");
                    cmd.Parameters.AddWithValue("@destination", "%" + tripdestination.Text + "%");
                }

                if (!string.IsNullOrWhiteSpace(tripactivity.Text))
                {
                    query.Append(" AND Type LIKE @type");
                    cmd.Parameters.AddWithValue("@type", "%" + tripactivity.Text + "%");
                }

                if (!string.IsNullOrWhiteSpace(tripgroupsize.Text))
                {
                    query.Append(" AND Capacity = @capacity");
                    cmd.Parameters.AddWithValue("@capacity", int.Parse(tripgroupsize.Text));
                }

                if (!string.IsNullOrWhiteSpace(tripstart.Text))
                {
                    query.Append(" AND StartDate = @startDate");
                    cmd.Parameters.AddWithValue("@startDate", DateTime.Parse(tripstart.Text));
                }

                if (!string.IsNullOrWhiteSpace(tripenddate.Text))
                {
                    query.Append(" AND EndDate = @endDate");
                    cmd.Parameters.AddWithValue("@endDate", DateTime.Parse(tripenddate.Text));
                }

                if (!string.IsNullOrWhiteSpace(tripstartprice.Text))
                {
                    query.Append(" AND price >= @startprice");
                    cmd.Parameters.AddWithValue("@startprice", float.Parse(tripstartprice.Text));
                }

                if (!string.IsNullOrWhiteSpace(tripendprice.Text))
                {
                    query.Append(" AND price <= @endprice");
                    cmd.Parameters.AddWithValue("@endprice", float.Parse(tripendprice.Text));
                }

                cmd.CommandText = query.ToString();

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                tripdatagrid.DataSource = dt;
                flowLayoutPanel1.Controls.Clear();

                foreach (DataRow row in dt.Rows)
                {
                    TripTile tile = new TripTile
                    {
                        TripTitle = row["Title"].ToString(),
                        TripType = row["Type"].ToString(),
                        Destination = row["Destination"].ToString(),
                        Capacity = row["Capacity"].ToString(),
                        Description = row["Description"].ToString(),
                        StartDate = row["startdate"].ToString(),
                        EndDate = row["EndDate"].ToString(),
                        TripPrice = row["price"].ToString()
                    };

                    // Capture the BookingID after booking
                    tile.bookClicked += (s, e2) =>
                    {
                        var result = bookTrip(row["TripId"].ToString());
                        if (result.BookingId != -1) // Only proceed if booking was successful
                        {
                            tile.BookingId = result.BookingId; // Store BookingID in TripTile
                            tile.Payment = result.TotalCost;  // Store TotalCost in TripTile
                        }
                    };
                    tile.payClicked += (s, e2) =>
                    {
                        if (tile.BookingId != -1 && tile.Payment > 0) // Ensure a booking exists
                        {
                            payBooking(tile.BookingId, tile.Payment);
                        }
                        else
                        {
                            MessageBox.Show("Please book the trip before paying.");
                        }
                    };
                    flowLayoutPanel1.Controls.Add(tile);
                }
            }
        }

        private void tripdatagrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
