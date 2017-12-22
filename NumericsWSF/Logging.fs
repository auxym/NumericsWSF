module ExcelNumerics.Logging

open System.Diagnostics

let tracer = TraceSource("NumericsWSF")

let traceVerbose o =
    tracer.TraceData(TraceEventType.Verbose, 0, (o :> obj))
    tracer.Flush ()
    o