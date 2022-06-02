using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPsReview.Data
{
    public class Employment
    {
        //An instance of this class will hold data about a person's employment
        //The code of thiss class is the definition of that data

        //The characteristics (data) of the class
        //  Title, SupervisorLevel, Years of employment within the company

        //there are 4 components of a class definition
        //  datafields (data members)
        //  property
        //  constructor
        //  behaviour (method)

        //data fields
        //  are storage areas in your class
        //  these are treated as variables
        //  these may be public, private, public readonly

        //Fields
        private string _Title;
        private double _Years;

        //property
        //these are access techniques to retrieve or set data in your class
        //  without directly touching the storage data field

        //A property is associated with a single instance of data
        //A property is public so it can be access by an outside user of the class
        //A property MUST have a get
        //A property MAY habve a set
        //  * if no set, the property is not changeable by the user; (readonly)
        //  -Why no set? --Commonly used for calculated data of the class
        //  the set can be either public or private
        //  public: the user can alter the contents of the data
        //  private: only code within the class can alter the contents of the data

        //fully implemented property
        //****** rdt = return data type
        //  a) a declared storage area (data field)
        //  b) a declared property signature (access rdt propertyname)
        //  c) a coded accessor (get) coding block : public
        //  d) an optional coded mutator (set) coding block : can be public or private
        //      if the set is private, the only way to place data into this
        //       property is via the constructor or a behaviour (method)
        
        //When do i want to use fully implemented property?:
        //  a) if you are storing the associate data in any explicitly declared
        //      data field
        //  b) if you are doing vallidation on incoming data
        //  c) creating a property that generates output from other data sources
        //      within the class (readonly property); this property would ONLY have
        //       an accessor

        //Properties
        public string Title
        {
            //a property is associated with a single piece of data
            get
            {
                //accessor
                //the get "coding block" will return the contents of a data field(s)
                //the return has syntax of return expression
                return _Title;
            }
            set
            {
                //mutator
                //the set "coding block" receives an incoming value and places it into
                //  the associated data field
                //during the setting, you might wish to validate the incoming data
                //during thge setting, you might wish to do some type of logical
                //  proccessing using the data to set another field
                //the incoming piece of data is referred to using keyword "value"

                //ensure that the incoming data is not null or empty or just whitespace
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("Title is a required piece of data.");
                }

                //data is considered valid
                _Title = value;
            }
        }

        //auto implemented property
        
        //these properties differ only in syntax
        //each property is responsible for a single piece of data
        // these properties do NOT reference a declared privcate data member
        //the sstem generates an internal storage area of the return data type
        //the system manages the internal storage for the accessor and mutator
        //!!!! THERE IS NO ADDITIONAL LOGIC APPLIED TO THE DATA VALUE !!!!!

        //using an enum for this field will automatically restrict the data values this
        // property can contain

        //syntax access rdt propertyname {get; [private]set;}
        public SupervisoryLevel Level { get; set; }

        public double Years
        {
            get { return _Years; }
            set
            {
                if (!Utilities.IsZeroPositive(value))
                {
                    throw new ArgumentOutOfRangeException($"Years of {value} is invalid. Must be 0 or greater.");
                }

                _Years = value;
            }
        }

        //constructor

        //this is used to initialize the physical object (instance) during its creation
        //the result of creation is to ensure that the coders getrs an instance in a "known state"
        //
        //if your class definition has NO constructor coded, then the data members and/or auto 
        //  implemented properties are set to the C# default data type value
        //
        //You can code one or more constructors in your class definition
        //!!!! IF YOU CODE A CONSTRUCTOR FOR THE CLASS, YOU ARE RESPONSIBLE FOR ALL CONSTRUCTORS USED BY THE CLASS !!!!
        //
        //generally, if you are going to code your own constructor(s), you code two types
        //  Default: this constructor does NOT take in any parameters
        //           this constructor mimics the default system constructor
        //  Greedy:  this constructor has a list of parameters, one for each property, declared
        //           for incoming data
        //
        //  (),(a),(b),(c),(d)(a,b)(a,c)(a,d) . . . 2^4 = 16 constructors
        //  (),(a,b,c,d)
        //
        //  syntax: accesstype classname([list of parameters]) {constructor code block}
        //
        //IMPORTANT: The constructor DOES NOT have a return datatype
        //           You DO NOT call a constructor directly, it is called using the new command
        //              => new classname(....);
        //

        //Default constructor
        public Employment()
        {
            //constructor body
            //  a) empty: values will be set to C# defaults
            //  b) you COULD assign literal values to your properties with this constructor

            //the values that you give your class data members/properties CAN be assigned directly
            //  to a data member
            //HOWEVER; if you have validated properties, you SHOULD consider saving your data 
            //          values via the property

            //YOU CAN code your validation logic within your constructors BECAUSE objects run your
            //  your constructor when it is created.
            //Placing your logic in the constructor could be done if your property has a private
            //  set OR if your public data member is a readonly data member
            //Private sets and readonly data members CAN NOT have their data altered directly
            Level = SupervisoryLevel.TeamMember;
            Title = "Unknown";

        }

        //Greedy Constructor
        public Employment(string title, SupervisoryLevel level, double years = 0.0)
        {
            //constructor body
            //  a) a parameter for each property
            //  b) you COULD code your validation in this constructor
            //  c) validation for public readonly data members MUST be done here
            //  d) validation for properties with a private set CAN be done here
            //      if not done in the property

            //default parameters

            //WHY? it allows the programmer to use your constructor/method without having to 
            //      specify all arguments in the code to your constructor/method
            //Location: end of parameter list
            //How many: as many as you wish
            //values for your default parameters MUST be a valid value
            //position and order of specified default parameters are important when the programmer
            //  uses the constructor/method.
            //default parameters CAN be skipped, HOWEVER, you still must account for the skipped
            //  parameter in your argument call list using commas
            //by giving the default parameter an argument value on the call, the
            //  constructor/method default value is overridden

            //syntax: datatype paramatername = default value
            //example: years on this constructor is a default parameter

            //example: skipped defaults (3 default parameters, second one is skipped
            //      ...(string requiredparam, int requiredparam, int default1 = 0,
            //      int default2 = 0, int default3 = 1)
            //
            //call: ...("required string", 25, 10, , 5) **default2 is skipped
            Title = title;
            Level = level;
            Years = years;  //evetually the data will be placed in _Years;

        }

        //Behaviours (a.k.a methods)
        //a behaviour is any method in your class
        //behaviour can be
        //  private (for use by the class only);
        //  public (for use by the outside user)
        //all rules about methods are in effect

        //a special method may be placed in your class to reflect the data stored by the instance
        //  (object) based on this class definition
        //this method is part of the system software and can be overridden by your own version of 
        //  the method

        public override string ToString()
        {
            //this string is known as "comma separated values (csv)" string
            //this string uses the get; of the property
            return $"{Title}, {Level}, {Years}";
        }

        public void SetEmployeeResponsibilityLevel(SupervisoryLevel level)
        {
            //this method, in this example would not be necessary as the access directly to the 
            //  Level (property) is public ( set; )
            //HOWEVER: IF the Level property had a private set; the outside user would NOT have 
            //          direct access to changing the property.
            //THEREFORE: a method (besides the constructor) would need to be supplied to allow the
            //           outside user the ability to alter the property value (if they so desired)

            //this assignment uses the set; of the property
            Level = level;
        }
    }
}
