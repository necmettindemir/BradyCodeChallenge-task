
- This is the code challenge task for BradyCode.

- The aim is to watch a folder path and do some operations with xml data in a console app when the input file is added/updated/deleted/etc..

- Becuase nothing is mentioned for choice of .Net version, .Net core 3.1 is preffered.
  .Net classic could be used of course.

- For the sake of the simplicity, solution is kept small as much as possible.

- Extendable scaffolding is designed so that many docs or new sub projects could be added to the solution BradyCodeChallange task.
  BCCconsoleApp, BCCclassLibrary and BCCxUnitTestApp are added to solution for now.


  BCCconsoleApp: Main app that uses BCCclassLibrary functions, models, etc.
  BCCclassLibrary: All functionalities are implemented in this project so that it can be used in another solution
  BCCxUnitTestApp: Test project to test functions in BCCclassLibrary



- Possible enchancements

	- All errors those are catched in exception block of try-catch could be logged 
	  or could be used to send message to an email address, queue mechanism, notification service, etc.

	- All works could be put in a seperated thread 
	  so that when it is required any other listener-like or watcher-like code could be embedded into main block of this app.

	- According to usage any database could be used.

	- If, in terms of usage, there are different scenarios the implementation could be designed as a service..
	  This service could be REST-based, tcpSocket-based, etc.

	  To make extendability the functions are collected in a library project and seperated from console app 
	  so that we can use referenced project, if required.

	- More test scenarios could be added according to story or functional requirements.
	  Anyway, a test project is added to the task..

	- Depenedency Injection could be implemented at many points of this task. 
	  e.g.1: txt-based any format could be handled as it is done with xml
	  e.g.2: settings could be taken from dbs as it is done with xml
	 

