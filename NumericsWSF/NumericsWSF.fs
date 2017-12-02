module NumericsWSF

open ExcelDna.Integration
open MathNet.Numerics.Interpolation

[<ExcelFunction(
    Name="NUM.INTERP.LINEAR",
    Caterory="Interpolation",
    Description="Linear interpolation based on MathNet.Numerics.LinearSpline",
)>]
let linInterp 
        ([<ExcelArgument(Description="x values on which interpolation is based")>]
        x1:float[])

        ([<ExcelArgument(Description="y values on which interpolation is based")>]
        y1:float[]) 
        
        ([<ExcelArgument(Description="x values at which interpolated values will be calculated")>]
        x2:float[]) =

    let interp = LinearSpline.Interpolate(x1, y1)
    Array.map interp.Interpolate x2
