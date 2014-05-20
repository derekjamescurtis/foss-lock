# SkyBee.Base.SharpArchitecture

Must of these base classes have been duplicated from the SharpArchitecture 
have been duplicated from the Sh#rpArchitecture project.  Very few modifications
have been made here, except for unflagging some of the properties are virtual 
(according to a lot of their inline documentation, absolutely all properties 
were marked as virtual because NHibernate requires everything to be marked as virtual
to enable lazy loading?? .. I guess.)

## Unit Testing Note

I should really write some unit tests for these, but
they're an almost direct copy from S#arpArchitecture,
which already has it's own unit tests that cover them..
I guess if I was feeling cool, I could just grab those and
drop them in here.