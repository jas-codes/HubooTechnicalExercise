using HubooTechnicalExercise;
using System.Net;

//One instance of httpclient, if an API this would be a singleton through DI
HttpClient HttpClient = new HttpClient();
var plate = new NumberPlate();

while (true)
{
    while (true)
    {
        Console.WriteLine("Please enter a drivers license number to check its history:");
        plate.Registration = Console.ReadLine();

        if (plate.IsPlateRegistrationValid)
        {
            break;
        }

        Console.WriteLine($"Oops! The number plate {plate.Registration} is invalid, try again!");
    }

    Console.WriteLine($"That plate looks correct, we'll see if it has any history now. \nChecking plate history for plate number {plate.Registration}...");

    var MotHttpClient = new MotHttpClient(HttpClient);
    (Car car, HttpStatusCode responseCode) = await MotHttpClient.GetMotHistoryAsync(plate.Registration);

    if (car is null && responseCode == HttpStatusCode.NotFound)
    {
        Console.WriteLine("We couldn't find that one, try another! \n");
        continue;
    }
    else if (car is null)
    {
        Console.WriteLine("Oops! Something went wrong! \n");
        continue;
    }

    Console.WriteLine("Here are the details:");
    Console.WriteLine($"Car Model {car.Model}");
    Console.WriteLine($"Car Make: {car.Make}");
    Console.WriteLine($"Car color: {car.PrimaryColour}");
    Console.WriteLine($"Number of previously failed MOT Tests: {car.GetNumberOfFailedMotTests()}");
    //assumption: this will never be null, a car has to have an MOT to be on the road
    Console.WriteLine($"Next MOT Due By: {car.GetNextMotDueDate().Date}");


    Console.WriteLine("Press any key to begin again...");
    Console.ReadKey();
    Console.WriteLine();
}