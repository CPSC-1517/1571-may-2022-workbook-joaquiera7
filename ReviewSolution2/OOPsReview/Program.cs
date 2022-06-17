// See https://aka.ms/new-console-template for more information
//this class is by default in the namespace of the project: OOPsReview

//an instance class needs to be created using the new command and the class constructor
//one needs to declare a variable of datatype Employment

//using the "using" statement means that one does NOT need to fully qualify on EACH usage of the class
using OOPsReview.Data;

//a fully qualified reference to Employment
//OOPsReview.Data.Employment myEmp = new OOPsReview.Data.Employment("Level 5 Programmer", SupervisoryLevel.Supervisor, 15.9);


Employment myEmp = new Employment("Level 5 Programmer", SupervisoryLevel.Supervisor, 15.9);
Console.WriteLine(myEmp.ToString()); //use the instance name to reference items within your class
Console.WriteLine($"{myEmp.Title}, {myEmp.Level}, {myEmp.Years}");


myEmp.SetEmploymentResponsibilityLevel(SupervisoryLevel.DepartmentHead);

Console.WriteLine(myEmp.ToString());

//testing (simulate a Unit test)
//Arrange (setup of your test data)
Employment Job = null;

//passing a reference variable to a method
//this passes the actual memory address of the data store to the method
//ANY changes done to the data store within the method WILL BE reflected 
//  in the data store WHEN you return from the method

CreateJob(ref Job);
Console.WriteLine(Job.ToString());

//passing value arguments to a method AND receiving a value result back from 
//  the method
//struct is a value data store
ResidentAddress Address = CreateAddress();
Console.WriteLine(Address.ToString());

//Act (execution of the test you wish to perform)
//test that we can create a Person (composite instance)
Person me = null;   //a variable capable of holding a Person instance
me = CreatePerson(Job, Address);

//Access (check your results)
Console.WriteLine($"{me.FirstName}, {me.LastName}, lives at {me.Address.ToString()}" +
    $" having a job count of {me.NumberOfPositions}");
Console.WriteLine("\nJobs: output via foreach loop\n");
foreach(var item in me.EmploymentPositions)
{
    Console.WriteLine(item.ToString());
}

Console.WriteLine("\nJobs: output via for loop\n");
for (int i = 0; i < me.EmploymentPositions.Count; i++)
{
    Console.WriteLine(me.EmploymentPositions[i].ToString());
}

//using Employment.Parse

string theRecord = "Boss,Owner,5.5";
Employment thePrasedRecord = Employment.Parse(theRecord);
Console.WriteLine(thePrasedRecord.ToString());

//using Employment .TryParse
thePrasedRecord = null;
if (Employment.TryParse(theRecord, out thePrasedRecord))
{
    //do whatever logic you need to do with the valid data
    Console.WriteLine(thePrasedRecord.ToString());
}
//if the TryParse failed, you would be handling it via your user friendly error handling code

//File I/O
//writing a comma separated value file (.csv file)
string pathname = WriteCSVFile();

//read a comma separated value file (.csv file)
//we will be using ReadAllLines(pathname); returns an array of strings; each 
//  array element is one line in your csv file.
//List<Employment> jobs = ReadCSVFile(pathname);

//writing a JSON file

//read a JSON file

void CreateJob(ref Employment job)
{
    //since the class May throw exceptions, you should have user friendly error handling
    try
    {
        job = new Employment(); //default constructor; new command takes a constructor as its reference
        //BECAUSE my properties have public set (mutators), I can "set" the value of the property 
        //  directly from the friver program
        job.Title = "Boss";
        job.Level = SupervisoryLevel.Owner;
        job.Years = 25.5;
        //OR

        //use the greedy constructor
        job = new Employment("Boss", SupervisoryLevel.Owner, 25.5);
    }
    catch (ArgumentNullException ex)
    {
        Console.WriteLine(ex.Message);
    }
    catch (ArgumentOutOfRangeException ex)
    {
        Console.WriteLine(ex.Message);
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }

}

ResidentAddress CreateAddress()
{
    ResidentAddress address = new ResidentAddress(10706, "106 st", "", "", "Edmonton", "AB");
    
    return address;
}

Person CreatePerson(Employment job, ResidentAddress address)
{
    //Person me = new Person("Don", "Welch", address, null);

    //one could add the job(s) to the instance of Person (me) after the instance is creted
    //  via the behaviour AddEmployment(Employment emp)
    //me.AddEmployment(Job);

    //OR

    //one could create a List <T> and add to the list<T> before creating the person instance
    List<Employment> employments = new List<Employment>();  //create the List<T> instance
    employments.Add(job);   //add an element to the List<T>
    Person me = new Person("Don", "Welch", address, employments); //using the greedy constructor

    //create additional jobs and load to Person
    Employment employment = new Employment("New Hire", SupervisoryLevel.Entry, 0.5);
    me.AddEmployment(employment);
    employment = new Employment("Team Head", SupervisoryLevel.TeamLeader, 5.2);
    me.AddEmployment(employment);
    employment = new Employment("Department IT Head", SupervisoryLevel.DepartmentHead, 6.8);
    me.AddEmployment(employment);
    return me;
}

string WriteCSVFile()
{
    string pathname = "";
    try
    {
        List<Employment> jobs = new List<Employment>();
        Employment theEmployment = new Employment("trainee", SupervisoryLevel.Entry, 0.5);
        jobs.Add(theEmployment);
        jobs.Add(new Employment("worker", SupervisoryLevel.TeamMember, 3.5));
        jobs.Add(new Employment("lead", SupervisoryLevel.TeamLeader, 7.4));
        jobs.Add(new Employment("dh new project", SupervisoryLevel.DepartmentHead, 1.5));

        //create a list of comma separated value strings
        //the contents of each string will be 3 values of Employment
        List<string> csvlines = new();

        //place all the instances of Employment in the collection of jobs
        //  in the csvlines using .ToString() of the Employment class
        foreach(var item in jobs)
        {
            csvlines.Add(item.ToString());
        }
        //write to a text file the csv lines
        //each line represents a Employment instance
        //you could use StreamWriter
        //HOWEVER within the File class there is a method that outputs a lists of stringfs
        //  all within ONE command. There is NO NEED for a StreamWriter instance.
        //the pathname is the minimum for the command
        //the file by default will be created in the same folder as your .exe file
        //you CAN alter the path name using relative addressing

        pathname = "../../../Employment.csv";
        File.WriteAllLines(pathname, csvlines);
        Console.WriteLine($"\nCheck out the CSV file at: {Path.GetFullPath(pathname)}");
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }

    return Path.GetFullPath(pathname);
}