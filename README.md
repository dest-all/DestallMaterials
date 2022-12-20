# DestallPackages

Different packages serving general purposes.

WheelProtection.* - packages to be imported in order not to write common utility code from scratch again and again.

  Queues - the most interesting of them. Contains two classes. 
  
    RateController: allows to limit some actions according within prescribed time ranges. May be used to comply with outer API service RPS (or any other type) constraints
    
    Recycler: works as a generic type pool with asynchronous items retrieval and ability to return the item back to it. May be used to setup connection pools, that don't throw if the limit is reached.

CodeGeneration - most grandioze of all in the repo. Allows to get compilation object of targetted project and bind it to blazor component, that will generate code, according to its content.
  
