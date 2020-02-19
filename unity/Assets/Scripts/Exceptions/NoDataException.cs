using System;

public class NoDataException : Exception {
    public NoDataException(string message) : base(message) {
   
    }
    
    public NoDataException() : base() {
   
    }

}