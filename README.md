EXAMPLE OF USAGE

    const String s = "The quick brown UberFoX jumps over the lazy dog and flies into space!";    
    var b64e = Convert.ToBase64String(Encoding.UTF8.GetBytes(s));
    var b64d = Encoding.UTF8.GetString(Convert.FromBase64String(b64e));
    var b191e = UBase191.EncodeString(s);
    var b191d = UBase191.DecodeString(b191e);
    Console.WriteLine("Text:\n" + s);
    Console.WriteLine("Base64 Encoded:\n" + b64e);
    Console.WriteLine("Base64 Decoded:\n" + b64d);
    Console.WriteLine("Base191 Encoded:\n" + b191e);
    Console.WriteLine("Base191 Decoded:\n" + b191d);
    Console.WriteLine("Base64 Size: " + b64e.Length);
    Console.WriteLine("Base191 Size: " + b191e.Length);
    Console.ReadKey();
    
PRINTS

    Text:
    The quick brown UberFoX jumps over the lazy dog and flies into space!
    Base64 Encoded:
    VGhlIHF1aWNrIGJyb3duIFViZXJGb1gganVtcHMgb3ZlciB0aGUgbGF6eSBkb2cgYW5kIGZsaWVzIGludG8gc3BhY2Uh
    Base64 Decoded:
    The quick brown UberFoX jumps over the lazy dog and flies into space!
    Base191 Encoded:
    àIáv÷3°Dÿ(J§7¼%j23H¥¬Btz3M¬^v¼I5Iß1Eí¬yY~x3§ÇH3cºEUxÑ7Åa¥>¿O½¡_iiÖ)EÉ'OYU"
    Base191 Decoded:
    The quick brown UberFoX jumps over the lazy dog and flies into space!
    Base64 Size: 92
    Base191 Size: 74
    
REMARKS

So this my Base191 I made it to convert binary into strings in a format smaller than base64 while still retaining ASCII characters within the 1-BYTE range without any use of control characters. As shown above it produces 74 characters for something Base64 would need 92 for and this becomes massive as the input gets bigger.
