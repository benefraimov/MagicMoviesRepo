Hey everyone, this project contain a React Client with Redux implementation and a .Net core 3 C# Backend folders.

This Backend would work with an SQL Server to save the data, so you can change the   

"ConnectionStrings": {
    "MagicMoviesConnection": "Server=localhost\\SQLEXPRESS;Database=MagicMoviesDB;Trusted_Connection=True;"
}

to your connection string with your preferred name.

!!!!!Important Note!!!!!: You can use this only for practice purposes, without using it as it yours!. 

In general it is a simulation of a local using system to manage movies and subscribers that can be assigned to those movies,
Which means that you can add movies, subscribers and workers to manage the system, beside that, you can even controll your 
workers permissions and determine their permission to make action in the system.

To run the Backend you have to use .net core 3 sdk, 3.1.3 version for accurancy.

And then you have to install dotnet ef tool for migration purposes and updating the DB.
To prevent fatal errors, i do recommend install dotnet ef
