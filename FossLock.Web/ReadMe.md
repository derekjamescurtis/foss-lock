# FossLock.Web

ASP.NET MVC5 Application that provides a user interface for managing license 
data, along with a restful-api that allows deployed applications to query 
their licensing data and submit activation requests.


## Namespaces

- __FossLock.Web__
    - __FossLock.Web.Controllers__
        - Standard ASP.NET MVC controller classes.
        - .__Base__
            - Base classes for implementing common functionality.
    - __Migrations__
        - Entity framework migrations and configurations.  
        - Note: Mappings are defined in a separate assembly (FossLock.BLL)
    - __Static__
        - Contains static CSS, JavaScript, Images and other data that will be 
            served directly by the web server.
    - __ViewModels__
        - Models passed to the Razor view engine for rendering.
        - __Converters__
            - Helper classes for converting data model classes to their
                appropriate view model representation and vice versa.
    - __Views__
        - The Razor view engine templates.


## Routing Note

I've opted for using Attribute-routing in this project.  Because there's lots
of common functionality in some of the controllers (especially those concerned 
with CRUD functionality), I've abstracted that out into base classes.  

The ASP.NET team decided that they would not support inheriting attribute routing 
from a base class, so we're forced to create wrappers for some of our methods and 
directly then call the base class, just so we can place an attribute on the method 
in the derrived class.