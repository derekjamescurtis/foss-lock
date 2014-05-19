# FossLock - An Open Source Copy Protection Library for .NET

## NOTE:

This project is nowhere near complete at the current time.  Check back 
or contribute some code.  This has been a side-project for me for quite some 
time... unfortunately, I just haven't had the time to pour into getting it done.

Good news, I have a small client project that I want to use this for.. so, it should
be getting finished up within the next 1-2 months.

## Quick Overview

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
in a small project a few years back.  Unfortunately the code base is pretty messy,
it hasn't been well-maintained for a few years.  Originally, I just wanted to make 
some updates to the project.. but after looking through the code.. I decided on 
doing a complete new system instead.

## Table of Contents

- Quick Start
- The License Server (FossLock.Web)
- The License Client (FossLock.Client)
- Appendix
    - Assembly Documentation    

## Quick Start Guide

- Step 1, wait for this to get done