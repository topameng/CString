# CString
C# 动态字符串,主要取代StringBuilder功能，但也包含大部分的string功能，可以再某些情况下取代字符串。
达到减少内存分配的目的

比如替代ngui UILable 成员mProcessedText:
https://github.com/topameng/mGUI/blob/ad1c27c8be15a5ab334cc9809e7a91baf0ab0fc5/Assets/NGUI/Scripts/UI/UILabel.cs#L94
