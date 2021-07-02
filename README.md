# API Benchmarks

[![standard-readme compliant](https://img.shields.io/badge/readme%20style-standard-brightgreen.svg?style=flat-square)](https://github.com/RichardLitt/standard-readme)

The purpose of this project is to show benchmark comparisons of various types of APIs including:
* Direct method calls.
* REST + JSON.
* GRPC + Protobuf.
* GraphQL.
* Messages using RabbitMQ.

## Table of Contents

- [Structure](#structure)
- [Security](#security)
- [Background](#background)
- [Development Environment, Etc](#development_environment_etc)
- [Contributing](#contributing)
- [License](#license)

## Structure
This product is structured with each different type of API getting a separate project, and one final project that is the CLI benchmarker.

## Security

SecurityCodeScan.VS2019 nuget package.

## Background

I have been doing a lot of reading about different kinds of APIs. This led me to wonder which is the "best" kind, 
and that line of thinking eventually evolved into trying to figure out how to determine which type of API to use 
for different situations. There is no single "best" API type that fits every situation, but rather the best one must
be chosen based on may factors. I then decided that I could at least do one of those factors scientifically, speed!
It's pretty easy to rank API types by the speed with which they can process information, so that's what this project 
does; it benchmarks the different types of API for comparison.

## Development Environment, Etc
* Visual Studio 2019.
* APIs, DLLs, CLI app.
* Source code is in Github.
* Project management is in Github.
* Docker and Docker-Compose.

## Testing

There will be numerous unit tests in the solution. Make sure they all pass!

## Contributing

Have at it! Please make a feature branch and add me to a PR.

If editing the Readme, please conform to the [standard-readme](https://github.com/RichardLitt/standard-readme) specification.

## License

The Unlicense, see https://unlicense.org. Written by Peter Hyde.