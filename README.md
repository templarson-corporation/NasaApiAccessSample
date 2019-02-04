# NASA API Access Sample
Demonstrates download and view of images using NASA's Mars Rover Photos API 

Use NasaImageDownloader console application to download images using NASA APO API.

NasaImageDownloader can download an image for specified date in /date parameter.
Example: NasaImageDownloader /api:apod /folder:C:\Users\MyUser\Downloads /datelist:C:\Users\MyUser\dates.txt

NasaImageDownloader can download images for multiple dates listed in a test file, specified in /datelist parameter.
Example: NasaImageDownloader /api:apod /folder:C:\Users\Mitko\Downloads /date:2017-02-26 

Use NasaImageViewer web application to view images for dates listed in a text file stored in App_Data folder of the ASP.NET application.

#Product Roadmap
- Other NASA API Support
- Containerization Support
