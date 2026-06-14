Imports System.Security.Policy

Public Class Farmer

    Public Property IsNewFarmer As Boolean
    Public Property FarmerCode As String
    Public Property FarmerName As String
    Public Property MobileNo As String

    Public Property AddressNo As String
    Public Property Moo As String
    Public Property VillageName As String
    Public Property SubDistrict As String
    Public Property District As String
    Public Property Province As String

    Public Property ContractNo As String

    Public ReadOnly Property ConcateAddress As String
        Get
            Return AddressNo &
                IIf(Moo <> "", " " & Moo, "") &
                IIf(VillageName <> "", " " & VillageName, "") &
                IIf(SubDistrict <> "", " " & SubDistrict, "") &
                IIf(District <> "", " " & District, "") &
                IIf(Province <> "", " " & Province, "")
        End Get
    End Property

End Class