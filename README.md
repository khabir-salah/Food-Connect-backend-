
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Food Connect - README</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            line-height: 1.6;
            margin: 0;
            padding: 0;
        }
        .container {
            padding: 20px;
        }
        h1, h2, h3 {
            color: #333;
        }
        p {
            margin-bottom: 1em;
        }
        ul {
            list-style-type: square;
            padding-left: 20px;
        }
        .screenshot {
            border: 1px solid #ddd;
            padding: 5px;
            margin: 10px 0;
            width: 100%;
            max-width: 600px;
        }
    </style>
</head>
<body>
    <div class="container">
        <h1>Food Connect</h1>

        <p>Welcome to <strong>Food Connect</strong>, a platform designed to bridge the gap between surplus food sources and those in need. Our mission is to reduce food waste and combat food insecurity by connecting individuals, families, organizations, restaurants, supermarkets, and other food suppliers with charities and food distribution points.</p>

        <h2>🌟 Overview</h2>

        <p>Food Connect is a comprehensive solution that tackles the critical issue of food waste and food insecurity. By leveraging technology, we facilitate the redistribution of surplus food to those who need it the most. This platform allows donors to easily donate food, and recipients can claim donations that have been approved by super admins or managers. Once a donation is claimed, the donor and recipient can communicate directly through the built-in chat feature or arrange a delivery to transport the food.</p>

        <h2>🚀 Features</h2>
        <ul>
            <li><strong>Multi-Role System:</strong> Five roles including Super Admin, Manager, Organizations, Families, and Individuals, each with specific permissions and responsibilities.</li>
            <li><strong>Food Donation & Approval Process:</strong> Donors can list food donations, which must be approved by a Super Admin or Manager before they become available for others to claim.</li>
            <li><strong>Chat System:</strong> Once a donation is claimed, the donor and recipient can chat to coordinate the handover of the food. This feature is powered by SignalR.</li>
            <li><strong>Email Verification System:</strong> I implemented email verification using SMTP, with Mailtrap for testing, to ensure that users are verified before they can fully access the platform's features.</li>
            <li><strong>Authentication & Authorization:</strong> Secure authentication using JWT, ensuring that only authorized users can access and interact with the system.</li>
            <li><strong>Advanced Architecture:</strong> The application is built using Onion Architecture, incorporating CQRS and Repository patterns for a scalable and maintainable codebase.</li>
            <li><strong>Database & Versioning:</strong> MySQL is used for data storage, and the application supports API versioning to manage updates seamlessly.</li>
        </ul>

        <h2>🛠️ Technologies Used</h2>
        <ul>
            <li><strong>Backend:</strong> ASP.NET Web API, C#, SignalR, Fluent Validation, Onion Architecture, CQRS, Repository Pattern, JWT Authentication, API Versioning</li>
            <li><strong>Frontend:</strong> Razor, HTML, CSS, JavaScript</li>
            <li><strong>Database:</strong> MySQL</li>
        </ul>

        <h2>📸 Screenshots</h2>
        <h3>Dashboard</h3>
        <img src="path_to_your_screenshot.png" alt="Dashboard" class="screenshot">

        <h3>Food Donation Page</h3>
        <img src="path_to_your_screenshot.png" alt="Donation Page" class="screenshot">

        <h3>Chat Interface</h3>
        <img src="path_to_your_screenshot.png" alt="Chat Interface" class="screenshot">

        <h2>💻 Getting Started</h2>

        <h3>Prerequisites</h3>
        <ul>
            <li>.NET 6 SDK</li>
            <li>MySQL Server</li>
        </ul>

        <h3>Installation</h3>
        <ol>
            <li>Clone the repository:
                <pre><code>git clone https://github.com/khabir-salah/food-connect.git
cd food-connect</code></pre>
            </li>
            <li>Configure the database connection in <code>appsettings.json</code>.</li>
            <li>Apply migrations to set up the database:
                <pre><code>dotnet ef database update</code></pre>
            </li>
            <li>Run the application:
                <pre><code>dotnet run</code></pre>
            </li>
            <li>Open your browser and navigate to <code>https://localhost:7252</code> to view the application.</li>
        </ol>

        <h2>🤝 Contributing</h2>
        <p>We welcome contributions to make Food Connect even better! If you have suggestions, please open an issue or submit a pull request.</p>

        <h2>📧 Contact</h2>
        <p>For any inquiries or further information, please reach out to us at <a href="mailto:abdulkabirsalahudeen19@gmail.com">abdulkabirsalahudeen19@gmail.com</a>.</p>

        <hr>

        <p>Thank you.</p>
    </div>
</body>
</html>
