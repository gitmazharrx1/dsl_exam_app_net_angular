# exam_app_net_angular
This test exam application was created by .NET Core Web API and Angular. 

Migration command:
  -  When you run the migration command, you must set ExamCore.Database class library as the Startup Project. 

Example command:
  -  dotnet ef migrations add InitDatabase --project ExamCore.Database -s ExamCore.Api -c DatabaseContext --verbose
  -  dotnet ef database update InitDatabase --project ExamCore.Database -s ExamCore.Api -c DatabaseContext --verbose
  -  dotnet ef migrations remove --project ExamCore.Database -s ExamCore.Api -c DatabaseContext --verbose
