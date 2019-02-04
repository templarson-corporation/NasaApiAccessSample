using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using NasaApiGateway;
using System.Globalization;

public partial class _Default : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string dateListFilePath = Server.MapPath("~/App_Data/dates.txt");

        if (!String.Empty.Equals(dateListFilePath))
        {
            if (File.Exists(dateListFilePath))
            {
                string[] fileLines = File.ReadAllLines(dateListFilePath);
                NasaApiGateway.NasaApodConsumer consumer = new NasaApiGateway.NasaApodConsumer();
                Apod apod;

                foreach (string line in fileLines)
                {
                    DateTime date;
                    if (DateTime.TryParse(line, out date))
                    {
                    }
                    else
                    {
                        try
                        {
                            CultureInfo provider = CultureInfo.InvariantCulture;
                            date = DateTime.ParseExact(line, "MM/dd/yy", provider);
                        }
                        catch
                        {
                            continue;
                        }
                    }
                    consumer.RetrieveImageToken(date, out apod);

                    TableRow row = new TableRow();
                    TableCell dateCell = new TableCell();
                    dateCell.Text = line;
                    row.Cells.Add(dateCell);

                    TableCell imageCell = new TableCell();
                    Image image = new Image();
                    image.ImageUrl = apod.url;
                    imageCell.Controls.Add(image);
                    row.Cells.Add(imageCell);

                    Images.Rows.Add(row);
                }
            }
        }
    }
}