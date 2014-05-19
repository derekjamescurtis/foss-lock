# FossLock - An Open Source Copy Protection Library for .NET

## NOTE:

This project is nowhere near complete at the current time.  Check back 
or contribute some code.  This has been a side-project for me for quite some 
time... unfortunately, I just haven't had the time to pour into getting it done.

Good news, I have a small client project that I want to use this for.. so, it should
be getting finished up within the next 1-2 months.

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

- Quick Start
- The License Server (FossLock.Web)
- The License Client (FossLock.Client)
- Appendix
    - Assembly Documentation    

## Quick Start Guide

- Here's how you get started--wait for this to get done or contribute some code.