// See https://aka.ms/new-console-template for more information

using EventSourceWithPostgres.Data;
using EventSourceWithPostgres.Data.Entity;

Console.WriteLine("App Start");


var context = new AppDbContext();
context.Events.Add(new Event(1,""));


class MyClass
{
    
}
