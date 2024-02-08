// ask for input
using System.Reflection.Metadata;

Console.WriteLine("Enter 1 to create data file.");
Console.WriteLine("Enter 2 to parse data.");
Console.WriteLine("Enter anything else to quit.");
// input response
string? resp = Console.ReadLine();

if (resp == "1")
{
    // create data file

    // ask a question
    Console.WriteLine("How many weeks of data is needed?");
    // input the response (convert to int)
    int weeks = int.Parse(Console.ReadLine());
    // determine start and end date
    DateTime today = DateTime.Now;
    // we want full weeks sunday - saturday
    DateTime dataEndDate = today.AddDays(-(int)today.DayOfWeek);
    // subtract # of weeks from endDate to get startDate
    DateTime dataDate = dataEndDate.AddDays(-(weeks * 7));
    // random number generator
    Random rnd = new Random();
    // create file
    StreamWriter sw = new StreamWriter("data.txt");

    // loop for the desired # of weeks
    while (dataDate < dataEndDate)
    {
        // 7 days in a week
        int[] hours = new int[7];
        for (int i = 0; i < hours.Length; i++)
        {
            // generate random number of hours slept between 4-12 (inclusive)
            hours[i] = rnd.Next(4, 13);
        }
        // M/d/yyyy,#|#|#|#|#|#|#
        // Console.WriteLine($"{dataDate:M/d/yy},{string.Join("|", hours)}");
        sw.WriteLine($"{dataDate:M/d/yyyy},{string.Join("|", hours)}");
        // add 1 week to date
        dataDate = dataDate.AddDays(7);
    }
    sw.Close();
}
else if (resp == "2")
{
        // verify file exists
        if (File.Exists("data.txt"))
        {
            // read file
            StreamReader sr = new StreamReader("data.txt");
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                // separate lines into date and sleep data
                string[] arr = line.Split(',');

                // display date
                DateTime parsedDate = DateTime.Parse(arr[0]);
                Console.WriteLine($"Week of {parsedDate:MMM}, {parsedDate:dd}, {parsedDate:yyyy}");
                
                // separate data into individual data points
                string sleepHours = arr[1];
                string[] sleepHoursArr = sleepHours.Split('|');

                // set preferred alignment scheme
                const int align = 3;

                // display data
                Console.WriteLine($"{"Su",align}{"Mo",align}{"Tu",align}{"We",align}{"Th",align}{"Fr",align}{"Sa",align}");
                Console.WriteLine($"{"--",align}{"--",align}{"--",align}{"--",align}{"--",align}{"--",align}{"--",align}");
                Console.WriteLine($"{sleepHoursArr[0],align}{sleepHoursArr[1],align}{sleepHoursArr[2],align}{sleepHoursArr[3],align}{sleepHoursArr[4],align}{sleepHoursArr[5],align}{sleepHoursArr[6],align}");
                Console.WriteLine();
            }
            sr.Close();
        }
        else
        {
            // default if no data file exists
            Console.WriteLine("File not found.");
        }    
}
