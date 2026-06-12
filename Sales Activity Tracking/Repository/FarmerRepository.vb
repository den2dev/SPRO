Public Class FarmerRepository
    Public Function GetFarmerList() As List(Of Farmer)

#Region "Mock data"
        Return New List(Of Farmer) From {
               New Farmer With {.FarmerCode = "F0001", .FarmerName = "สมชาย ใจดี", .MobileNo = "0812345678"},
               New Farmer With {.FarmerCode = "F0002", .FarmerName = "สมศรี รุ่งเรือง", .MobileNo = "0823456789"},
               New Farmer With {.FarmerCode = "F0003", .FarmerName = "วิชัย ทองสุข", .MobileNo = "0834567890"},
               New Farmer With {.FarmerCode = "F0004", .FarmerName = "สุภาพร บุญมี", .MobileNo = "0845678901"},
               New Farmer With {.FarmerCode = "F0005", .FarmerName = "ประสิทธิ์ แก้วดี", .MobileNo = "0856789012"},
               New Farmer With {.FarmerCode = "F0006", .FarmerName = "อรทัย ศรีสุข", .MobileNo = "0867890123"},
               New Farmer With {.FarmerCode = "F0007", .FarmerName = "ธนพล แสนดี", .MobileNo = "0878901234"},
               New Farmer With {.FarmerCode = "F0008", .FarmerName = "มาลี พูนทรัพย์", .MobileNo = "0889012345"},
               New Farmer With {.FarmerCode = "F0009", .FarmerName = "กิตติชัย เจริญผล", .MobileNo = "0890123456"},
               New Farmer With {.FarmerCode = "F0010", .FarmerName = "สุดารัตน์ ทองคำ", .MobileNo = "0801234567"}
           }
#End Region

    End Function

    'Public Function SearchFarmer(keyword As String) As IEnumerable(Of FarmerModel)

    'End Function

    Public Function GetFarmer(farmercode As String) As Farmer


#Region "Mock"
        Return New Farmer With {
            .FarmerCode = farmercode,
            .FarmerName = "สมชาย ใจดี",
            .MobileNo = "0812345678",
            .AddressNo = "99/1",
            .Moo = "5",
            .SubDistrict = "บ้านใหม่",
            .District = "เมือง",
            .Province = "ขอนแก่น",
            .ContractNo = "C20250001"
        }
#End Region


    End Function



    Public Function CreateFarmer(model As NewFarmerViewModel) As Farmer

#Region "Mock"
        Return New Farmer With {
            .FarmerCode = "F26005",
            .FarmerName = model.FarmerName,
            .MobileNo = model.MobileNo,
            .AddressNo = model.AddressNo,
            .Moo = model.Moo,
            .SubDistrict = model.SubDistrictCode,
            .District = model.DistrictCode,
            .Province = model.ProvinceCode,
            .ContractNo = ""
        }
#End Region

    End Function

End Class
