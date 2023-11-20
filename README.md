# gRPC-on-.NET
gRPC is a language agnostic, high-performance Remote Procedure Call (RPC) framework.

The main benefits of gRPC are:

- Modern, high-performance, lightweight RPC framework.
- Contract-first API development, using Protocol Buffers by default, allowing for language agnostic implementations.
- Tooling available for many languages to generate strongly-typed servers and clients.
- Supports client, server, and bi-directional streaming calls.
-Reduced network usage with Protobuf binary serialization.

These benefits make gRPC ideal for:

- Lightweight microservices where efficiency is critical.
- Polyglot systems where multiple languages are required for development.
- Point-to-point real-time services that need to handle streaming requests or responses.


## Overview
gRPC JSON transcoding is an extension for ASP.NET Core that creates RESTful JSON APIs for gRPC services. Once configured, transcoding allows apps to call gRPC services with familiar HTTP concepts:

- HTTP verbs
- URL parameter binding
- JSON requests/responses

gRPC can still be used to call services.

# Usage
# Usage
1. Add a package reference to [Microsoft.AspNetCore.Grpc.JsonTranscoding](https://www.nuget.org/packages/Microsoft.AspNetCore.Grpc.JsonTranscoding).
2. Register transcoding in server startup code by adding AddJsonTranscoding: In the `Program.cs` file, change `builder.Services.AddGrpc();` to `builder.Services.AddGrpc().AddJsonTranscoding();`.
3. Create the directory structure `/google/api` in the project directory that contains the `.csproj` file.
4. Add [google/api/http.proto](https://github.com/dotnet/aspnetcore/blob/main/src/Grpc/JsonTranscoding/test/testassets/Sandbox/google/api/http.proto) and [google/api/annotations.proto](https://github.com/dotnet/aspnetcore/blob/main/src/Grpc/JsonTranscoding/test/testassets/Sandbox/google/api/annotations.proto)files to the `/google/api` directory.

```proro
syntax = "proto3";

option csharp_namespace = "GrpcServiceTranscoding";
import "google/api/annotations.proto";


package greet;

// The greeting service definition.
service Greeter {
  rpc SayHello (HelloRequest) returns (HelloReply) {
    option (google.api.http) = {
      get: "/v1/greeter/{name}"
    };
  }
}

// The request message containing the user's name.
message HelloRequest {
  string name = 1;
}

// The response message containing the greetings.
message HelloReply {
  string message = 1;
}
```

The SayHello gRPC method can now be invoked as gRPC and as a JSON Web API:

- Request: GET /v1/greeter/world
- Response: { "message": "Hello world" }

## HTTP protocol
The ASP.NET Core gRPC service template, included in the .NET SDK, creates an app that's only configured for HTTP/2. HTTP/2 is a good default when an app only supports traditional gRPC over HTTP/2. Transcoding, however, works with both HTTP/1.1 and HTTP/2. Some platforms, such as UWP or Unity, can't use HTTP/2. To support all client apps, configure the server to enable HTTP/1.1 and HTTP/2.

```json
{
  "Kestrel": {
    "EndpointDefaults": {
      "Protocols": "Http1AndHttp2"
    }
  }
}
```
Enabling HTTP/1.1 and HTTP/2 on the same port requires TLS for protocol negotiation.

## gRPC JSON transcoding vs gRPC-Web
Both transcoding and gRPC-Web allow gRPC services to be called from a browser. However, the way each does this is different:

1. gRPC-Web lets browser apps call gRPC services from the browser with the gRPC-Web client and Protobuf. gRPC-Web requires the browser app to generate a gRPC client and has the advantage of sending small, fast Protobuf messages.
2. Transcoding allows browser apps to call gRPC services as if they were RESTful APIs with JSON. The browser app doesn't need to generate a gRPC client or know anything about gRPC.
The previous Greeter service can be called using browser JavaScript APIs:

```js
var name = nameInput.value;

fetch('/v1/greeter/' + name)
  .then((response) => response.json())
  .then((result) => {
    console.log(result.message);
    // Hello world
  });

```