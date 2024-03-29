/*
* Copyright 2007 ZXing authors
*



*

*





*/
using System;
using Result = Tag4s.Zxing.Result;
namespace Tag4s.Zxing.client.result
{
	
	/// <summary> Implements the "MECARD" address book entry format.
	/// 
	/// Supported keys: N, SOUND, TEL, EMAIL, NOTE, ADR, BDAY, URL, plus ORG
	/// Unsupported keys: TEL-AV, NICKNAME
	/// 
	/// Except for TEL, multiple values for keys are also not supported;
	/// the first one found takes precedence.
	/// 
	/// Our understanding of the MECARD format is based on this document:
	/// 
	/// http://www.mobicode.org.tw/files/OMIA%20Mobile%20Bar%20Code%20Standard%20v3.2.1.doc 
	/// 
	/// </summary>
	/// <author>  
	/// </author>
	/// <author>
	/// </author>
	sealed class AddressBookDoCoMoResultParser:AbstractDoCoMoResultParser
	{
		
		public static AddressBookParsedResult parse(Result result)
		{
			System.String rawText = result.Text;
			if (rawText == null || !rawText.StartsWith("MECARD:"))
			{
				return null;
			}
			System.String[] rawName = matchDoCoMoPrefixedField("N:", rawText, true);
			if (rawName == null)
			{
				return null;
			}
			System.String name = parseName(rawName[0]);
			System.String pronunciation = matchSingleDoCoMoPrefixedField("SOUND:", rawText, true);
			System.String[] phoneNumbers = matchDoCoMoPrefixedField("TEL:", rawText, true);
			System.String[] emails = matchDoCoMoPrefixedField("EMAIL:", rawText, true);
			System.String note = matchSingleDoCoMoPrefixedField("NOTE:", rawText, false);
			System.String[] addresses = matchDoCoMoPrefixedField("ADR:", rawText, true);
			System.String birthday = matchSingleDoCoMoPrefixedField("BDAY:", rawText, true);
			if (birthday != null && !isStringOfDigits(birthday, 8))
			{
				// No reason to throw out the whole card because the birthday is formatted wrong.
				birthday = null;
			}
			System.String url = matchSingleDoCoMoPrefixedField("URL:", rawText, true);
			
			// Although ORG may not be strictly legal in MECARD, it does exist in VCARD and we might as well
			// honor it when found in the wild.
			System.String org = matchSingleDoCoMoPrefixedField("ORG:", rawText, true);
			
			return new AddressBookParsedResult(maybeWrap(name), pronunciation, phoneNumbers, emails, note, addresses, org, birthday, null, url);
		}
		
		private static System.String parseName(System.String name)
		{
			int comma = name.IndexOf(',');
			if (comma >= 0)
			{
				// Format may be last,first; switch it around
				return name.Substring(comma + 1) + ' ' + name.Substring(0, (comma) - (0));
			}
			return name;
		}
	}
}