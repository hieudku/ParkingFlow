<h1>ParkingFlow</h1>

<p>
ParkingFlow is a web parking management application built using ASP.NET Core MVC and SQL Server. It provides basic functionality for users to find and book parking spaces and for administrators to manage available slots and bookings. This project is suitable for educational purposes, prototyping, or as a starting point for building a full-featured parking management system.
</p>

<hr/>

<h2>Project Structure</h2>

<ul>
  <li><strong>Backend:</strong> ASP.NET Core MVC with Entity Framework Core</li>
  <li><strong>Frontend:</strong> Razor views, Bootstrap</li>
  <li><strong>Authentication:</strong> ASP.NET Identity</li>
  <li><strong>Database:</strong> SQL Server (Development), Azure SQL Server (Production)</li>
  <li><strong>Payment:</strong> Stripe integration for booking checkout</li>
  <li><strong>Hosting:</strong> Azure App Service</li>
</ul>

<hr/>

<h2>Setup Instructions</h2>

<h3>1. Clone the repository</h3>

<pre><code>git clone https://github.com/hieudku/parkingflow.git</code></pre>

<h3>2. Open the project</h3>

<p>
Open the solution in Visual Studio 2022 (or later). Make sure you have the required .NET SDK installed ( .NET 6 or .NET 8 depending on your project settings).
</p>

<h3>3. Set up the database connection</h3>

<p>
In <code>appsettings.json</code>, configure the connection string(localdb):
</p>

<pre><code>
"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=ParkingFlow_db;Trusted_Connection=True;MultipleActiveResultSets=true"
}
</code></pre>

<p>
If deploying to Azure, store this value in your App Service Configuration settings.
</p>

<h3>4. Configure Stripe keys</h3>

<p>
Add your Stripe API keys in <code>appsettings.json</code> or as environment variables (recommended for good practice):
</p>

<pre><code>
"Stripe": {
  "SecretKey": "your-stripe-secret-key",
  "PublishableKey": "your-stripe-publishable-key"
}
</code></pre>

<h3>5. Run database migrations</h3>

<pre><code>Update-Database</code></pre>

<p>
This command will create the necessary tables in your SQL Server database using Entity Framework migrations.
</p>

<h3>6. Run the application</h3>

<p>
Set the project as the startup project and run via Visual Studio or with the CLI:
</p>

<pre><code>dotnet run</code></pre>

<p>
Visit <code>https://localhost:5001</code> in your browser(localhost might differ).
</p>

<hr/>

<h2>User Roles</h2>

<ul>
  <li><strong>Admin</strong>: Can add/edit/delete parking slots and view all bookings</li>
  <li><strong>User</strong>: Can browse available parking, make bookings, and manage their own reservations</li>
</ul>

<p>
Admin users must be seeded or manually assigned in the database.
</p>

<hr/>

<h2>Environment Variables (Optional)</h2>

<p>
You can optionally store sensitive config values outside <code>appsettings.json</code> for security (in secrets.json):
</p>

<ul>
  <li><code>ConnectionStrings__DefaultConnection</code></li>
  <li><code>Stripe__SecretKey</code></li>
  <li><code>Stripe__PublishableKey</code></li>
</ul>

<hr/>

<h2>Deployment Notes</h2>

<ul>
  <li>The application can be hosted on Azure App Service.</li>
  <li>Database should be provisioned using Azure SQL or equivalent.</li>
  <li>Use Custom Domains and TLS/SSL bindings for production.</li>
</ul>

<hr/>


<hr/>

<h2>Author</h2>

<ul>
  <li>Hieu Cu (Lead)</li>
  <li>Shivam Arora (Tester)</li>
</ul>

