# Backend-Interview

This is my project in the Interview with Mr. Son Phan

This project has an api /api/math/getAnswer to sort interger array.
Convention:
1) Top 10 element have max value are in center of array
2) Next top 10 elements have max value are in begin of array
3) Next top 10 elements have max value are in end of array

***Note***
Please change your local *URL* in 
var allowDomain = Configuration.GetValue<string>("AllowedHosts");
            app.UseCors(builder => builder
                  .WithOrigins("http://localhost:3000")
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials()
            );
of Startup.cs project to make Frontend access the api.
  
  Thanks!
