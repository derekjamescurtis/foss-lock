# FossLock - An Open Source Copy Protection Library for .NET/Mono

## Synopsis

FossLock is a simple copy protection library intended for use by ISV's distributing 
closed-source applications or possibly by enterprises wishing to maintain control over
use of internal applications.

It includes a web-based license management app, and supports several different license
activation mechanisms, including automatic web-activation.

A goal of this project, from the beginning, was to run both on Microsoft's .NET
framework, as well as on the Mono framework.  As such, it has built in functionality
for detecting hardware on both MS-Windows and Linux.

## NOTE:

This project is nowhere near complete at the current time.  Check back 
or contribute some code.  This has been a side-project for me for quite some 
time... unfortunately, I just haven't had the time to pour into getting it done, 
as it's a decent sized project.  There are currently a few issues existing 
mostly dealing with the HardwareFingerprint project, if anyone has interest
in seeing this finished up more quickly, you could take a look at and submit
some merge requests.

## Quick Overview

FossLock is a simple copy protection system intended for use by Independent Software
Vendors, distributing .NET-based applications.

There are 2 major components of the application 

- FossLock.Web
- FossLock.Client

__FossLock.Web__ is an ASP.NET MVC 4 application and Web API for entering
licensing information.  The Web API component is used by FossLock.Client for 
web activation of licenses.

__FossLock.Client__ is a class library that you embed with your assemblies.  
The main job of FossLock.Client is to collect hardware information validate licensing
information and possibly (depending on configuration) to communicate with FossLock.Web.

## Reasoning

The idea for this project originally came from 
[ActiveLock](http://www.activelocksoftware.com), which I had personally used
in a small project a few years back.  It hasn't been updated, and the codebase is 
fairly messy.. I originally planned on contributing some commits back to 
the project.. before eventually just deciding on building a fresh system with
some of the basic ideas borrowed from that project, but a completely new code
based.

My biggest frusteration with their software was not with integration into my software,
but really the very buggy license manager application, and the fact that the license
manager was a Winforms app, and not something web-based.  

## Table of Contents

### Quick Start

- Here's how you get started--wait for this to get done or contribute some code.

### The License Server (FossLock.Web)
### The License Client (FossLock.Client)

### Appendix
#### [License - BSD 3-Clause](http://opensource.org/licenses/BSD-3-Clause)

Copyright (c) 2012-2014, Derek J. Curtis, Greg F. Martin, Ethan K. Hall
All rights reserved.

Redistribution and use in source and binary forms, with or without modification, are permitted provided 
that the following conditions are met:

1. Redistributions of source code must retain the above copyright notice, 
this list of conditions and the following disclaimer.
2. Redistributions in binary form must reproduce the above copyright notice, this 
list of conditions and the following disclaimer in the documentation and/or other 
materials provided with the distribution.
3. Neither the name of the copyright holder nor the names of its contributors may be used 
to endorse or promote products derived from this software without specific prior written permission.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR 
IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS 
FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE 
FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT 
LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS 
INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR 
TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF 
ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.


#### Contributers
- Derek J. Curtis
- Greg F. Martin
- Ethan K. Hall

#### Assembly Reference
##### Applications
- FossLock.Web
    - Web-based license manager app.
    - Built on ASP.NET MVC 4.
    - Provides API for automated web-activation.

##### Data Layers
- FossLock.Core
    - Library containing 'core' data types.
    - Fancy way of saying it contains lots of enum definitions.
- FossLock.Model
    - POCO entity classes.
- FossLock.DAL
    - Data access library.
    - Currently only supports entity framework.
    - Includes DbContext class and fluent type configurations.
- FossLock.BLL
    - Service layer library.
    - Applies business validation rules and communicates between FossLock.Web and FossLock.DAL

##### Other
- FossLock.Client
    - Library to be embedded with your deployed application.
    - Handled authorizing 
- FossLock.LicenseHandler
    - .. not really sure what we were originally thinking here.
    - Looks like this can probably die, as FossLock.Client likely handles what this was supposed to.
- FossLock.HardwareFingerprint
    - Library for generating hardware identifiers.
    - Supports both Windows and Linux.
- FossLock.Test.*
    - These assemblies represent the test suite for FossLock.
    - The third part of the identifier will match an assembly within the rest of the project.
    - Tests are named based on the following convention: __filename__.__testtype__ (ex, unit tests for FossLock.BLL.Service.GenericService.cs can be found in FossLock.Test.BLL.GenericService_UnitTests.cs)
