# FossLock.Model

## Synopsis

This whole proejct just contains the POCO classes used to generate the 
database for EF6, and also are also used as serializable transport 
objects for communicating with the web server's API.

## Validation / Entity Framework Config

Everything was moved from data attributes to the fluent api in the DAL assembly.

Further validation should be provided by type-specific repositories in the BLL 
assembly.

## Base Classes

Must of these base classes have been duplicated from the SharpArchitecture 
have been duplicated from the Sh#rpArchitecture project.  Very few modifications
have been made here, except for unflagging some of the properties are virtual 
(their documentation indicates they were only virtual support lazy loading 
in nhibernate.. which I don't really understand since some of them don't 
actually map out to database fields, but I'll believe whoever wrote their
documentation).

## SharpArchitecture License

New BSD License for S#arp Architecture from Codai, Inc.

Copyright (c) 2009, Codai, Inc.
All rights reserved.

Redistribution and use in source and binary forms, with or without modification,
are permitted provided that the following conditions are met:

* Redistributions of source code must retain the above copyright notice,
    this list of conditions and the following disclaimer.

* Redistributions in binary form must reproduce the above copyright notice,
    this list of conditions and the following disclaimer in the documentation
    and/or other materials provided with the distribution.

* Neither the name of Codai, Inc., nor the names of its
    contributors may be used to endorse or promote products derived from this
    software without specific prior written permission.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR
ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
(INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON
ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
(INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.