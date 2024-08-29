# Food Connect

Welcome to **Food Connect**, a platform designed to bridge the gap between surplus food sources and those in need. Our mission is to reduce food waste and combat food insecurity by connecting individuals, families, organizations, restaurants, supermarkets, and other food suppliers with charities and food distribution points.

## 🌟 Overview

Food Connect is a comprehensive solution that tackles the critical issue of food waste and food insecurity. By leveraging technology, we facilitate the redistribution of surplus food to those who need it the most. This platform allows donors to easily donate food, and recipients can claim donations that have been approved by super admins or managers. Once a donation is claimed, the donor and recipient can communicate directly through the built-in chat feature or arrange a delivery to transport the food.

## 🚀 Features

- **Multi-Role System**: Five roles including Super Admin, Manager, Organizations, Families, and Individuals, each with specific permissions and responsibilities.
- **Food Donation & Approval Process**: Donors can list food donations, which must be approved by a Super Admin or Manager before they become available for others to claim.
- **Chat System**: Once a donation is claimed, the donor and recipient can chat to coordinate the handover of the food. This feature is powered by SignalR.
- **Booking & Delivery**: Recipients can book a ride for food delivery directly through the platform.
- **Authentication & Authorization**: Secure authentication using JWT, ensuring that only authorized users can access and interact with the system.
- **Advanced Architecture**: The application is built using Onion Architecture, incorporating CQRS and Repository patterns for a scalable and maintainable codebase.
- **Database & Versioning**: MySQL is used for data storage, and the application supports API versioning to manage updates seamlessly.

## 🛠️ Technologies Used

- **Backend**: ASP.NET Web API, C#, SignalR, Fluent Validation, Onion Architecture, CQRS, Repository Pattern, JWT Authentication, API Versioning
- **Frontend**: Razor, HTML, CSS, JavaScript
- **Database**: MySQL

## 📸 Screenshots

### Dashboard
![Dashboard](path_to_your_screenshot.png)

### Food Donation Page
![Donation Page](path_to_your_screenshot.png)

### Chat Interface
![Chat Interface](path_to_your_screenshot.png)

## 💻 Getting Started

### Prerequisites

- .NET 6 SDK
- MySQL Server

### Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/yourusername/food-connect.git
   cd food-connect

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

        ##🤝 Contributing
        *We welcome contributions to make Food Connect even better! If you have suggestions, please open an issue or submit a pull request.

        ##📧 Contact
        **For any inquiries or further information, please reach out to us at <a href="mailto:abdulkabirsalahudeen19@gmail.com">abdulkabirsalahudeen19@gmail.com<\a>.

        Thank you.

