using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server.Web
{
    internal static class Home
    {
        internal static void MapHomeEndPoints(this WebApplication app)
        {
            app.MapGet("/", Index);
        }
        internal static IResult Index()
        {
            string html = @"<!DOCTYPE html>
<html lang=""en"">
<head>
    <meta charset=""utf-8"" />
    <title>Alaska Tea & Cohocolate</title>
    <meta name=""viewport"" content=""width=device-width, initial-scale=1"" />
    <link href=""/css/bootstrap.min.css"" rel=""stylesheet"" />
    <style>
        body {
            padding-top: 60px;
            background-color: #fefefe;
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
        }

        .navbar-brand {
            font-weight: bold;
            color: #4CAF50 !important;
        }

        footer {
            margin-top: 4rem;
            padding: 2rem 0;
            background-color: #f1f1f1;
            text-align: center;
            color: #888;
        }
    </style>
</head>
<body>
    <nav class=""navbar navbar-expand-lg navbar-light bg-light fixed-top shadow-sm"">
        <div class=""container"">
            <a class=""navbar-brand"" href=""/"">Alaska</a>
            <span class=""navbar-text"">
                Tea & Chocolate
            </span>
        </div>
    </nav>

    <main role=""main"" class=""container"">
<div class=""text-center mt-5"">
    <h1 class=""display-4 fw-bold"">Welcome to Alaska</h1>
    <p class=""lead"">Crafted delicately with the finest tea leaves and rich chocolate blends.</p>
    <img src=""/images/ps3.png"" class=""img-fluid rounded shadow mt-4"" alt=""Alaska Tea & Chocolate"" style=""max-height: 400px;"">
</div>

<section class=""mt-5"">
    <div class=""row"">
        <div class=""col-md-6"">
            <h2>About Alaska</h2>
            <p>
                At <strong>Alaska</strong>, we believe every sip should tell a story. Our carefully selected tea leaves
                and premium chocolate are sourced sustainably and crafted with passion to bring you warmth, comfort,
                and a touch of luxury.
            </p>
            <p>
                Whether you crave a calming herbal infusion or a bold chocolate brew, Alaska offers an experience
                that soothes the soul and excites the senses.
            </p>
        </div>
        <div class=""col-md-6"">
            <img src=""/images/chocolate.jpeg""
                 class=""img-fluid rounded shadow"" alt=""Cozy Tea & Chocolate"" />
        </div>
    </div>
</section>

<section class=""mt-5 text-center"">
    <h2>Explore Our Signature Blends</h2>
    <p class=""text-muted"">From floral teas to velvety chocolates, every cup is a moment of indulgence.</p>
</section>
    </main>

    <footer>
        <div class=""container"">
            <p>&copy; @DateTime.Now.Year Alaska. All rights reserved.</p>
        </div>
    </footer>

    <script src=""/js/bootstrap.bundle.min.js""></script>
</body>
</html>";
            return Results.Content(html, "text/html", Encoding.UTF8);
        }
    }
}
