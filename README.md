# ExceptionHandler
ExceptionHandler

This Library is a middleware which treats exceptions. It has few Defined Exceptions mapped to the following HttpStatusCodes:

204 NoContent - Status Code can be used when Request sucessfully, but with no returned content.

422 UnprocessableEntity - can be used when the server understands the content type of the request entity, and the syntax of the request entity is correct,
but it was unable to process the contained instructions

409 Conflict - can be used when Request sucessfully, but with no returned content.

404 NotFound - The server has not found anything matching the Request-URI./The Requested resource does not exists. The key you are looking for is not found.

Eg:  "The link that directed the user to your server resource have a typographical error in it.", "The file does not exist in the correct location on the server/The resource was moved or deleted on the server."
                                                                                 
400 BadRequest - Missing data, domain validation, invalid formatting.

Eg: The input sent by the client doesn’t honor the rules of the http protocol." Can be used for Can be used for ArgumentNullException.

500 InternalServerError - is a generic "catch-all" response. Indicates that the server encountered an unexpected condition that prevented it from fulfilling the request.


# How to use it:
Step1: install ExceptionHandler package from 

Step2: Open Startup.cs file of your project,

+ in ConfigureServies(IServiceCollection services) method, add services.AddErrorHandler();

 + in Configure(IApplicationBuilder app, IWebHostEnvironment env) add app.UseErrorHandler();
 
 Step3: Use defined exceptions in ExceptionHandler as you need
 
 If you need an example, see StorageBox2
