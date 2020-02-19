loosely based off of a simple example from 
https://c-sharpcorner.com/article/creating-simple-cascaing-dropdownlist-in-asp-net-core-mvc-with-new-tag-helpers/

only I'm using mvc 3.0 and adapting the code to better suit my needs as I learn how to manipulate dropdowns 
with JQuery/AJAX.  I'll be using what I learn to make my own inventory webapp that will live on https://tamanaka.com/greenthumb

so with asp.net3 you can't dotnet ef migrations unless you specifically install the tools for it. it is no longer included.

to install the tools
dotnet tool install --global dotnet-ef
dont forget to add Microsoft.EntityFrameworkCore.Design to your project using nuget!
