using System;
using System.Drawing;

namespace DataUtils
{
	public class HighlightDescriptor
	{
		public HighlightDescriptor(string token, Color color, string description, Font font, DescriptorType descriptorType, DescriptorRecognition dr, bool useForAutoComplete)
		{
			if (descriptorType == DescriptorType.ToCloseToken)
			{
				throw new ArgumentException("You may not choose ToCloseToken DescriptorType without specifing an end token.");
			}
			Color = color;
			Autocomplete = token;
            Font = font;
            Token = token;
			Description = description;
			Result = "";
			Type = "Тип отсутствует";
			DescriptorType = descriptorType;
			DescriptorRecognition = dr;
			CloseToken = null;
			UseForAutoComplete = useForAutoComplete;
		}
		public HighlightDescriptor(string token, string autocomplit, Color color, string type, string description, string result, Font font, DescriptorType descriptorType, DescriptorRecognition dr, bool useForAutoComplete)
		{
			if (descriptorType == DescriptorType.ToCloseToken)
			{
				throw new ArgumentException("You may not choose ToCloseToken DescriptorType without specifing an end token.");
			}
			Color = color;
			Autocomplete = autocomplit;
            Font = font;
			Token = token;
			Description = description;
			Result = result;
			Type = type;
			DescriptorType = descriptorType;
			DescriptorRecognition = dr;
			CloseToken = null;
			UseForAutoComplete = useForAutoComplete;
		}
		public HighlightDescriptor(string token, string closeToken, Color color, Font font, DescriptorType descriptorType, DescriptorRecognition dr, bool useForAutoComplete)
		{
			Color = color;
            Font = font;
			Token = token;
			DescriptorType = descriptorType;
			CloseToken = closeToken;
			DescriptorRecognition = dr;
			UseForAutoComplete = useForAutoComplete;
		}
		public readonly Color Color;
		public readonly Font Font;
		public readonly string Token;
		public readonly string CloseToken;
		public readonly string Type;
		public readonly string Description;
		public readonly string Result;
		public readonly string Autocomplete;
		public readonly DescriptorType DescriptorType;
		public readonly DescriptorRecognition DescriptorRecognition; 
		public readonly bool UseForAutoComplete;
	}

	
	public enum DescriptorType
	{
		/// <summary>
		/// Causes the highlighting of a single word
		/// </summary>
		Word,
		/// <summary>
		/// Causes the entire line from this point on the be highlighted, regardless of other tokens
		/// </summary>
		ToEOL,
		/// <summary>
		/// Highlights all text until the end token;
		/// </summary>
		ToCloseToken
	}

	public enum DescriptorRecognition
	{
		/// <summary>
		/// Only if the whole token is equal to the word
		/// </summary>
		WholeWord,
		/// <summary>
		/// If the word starts with the token
		/// </summary>
		StartsWith,
		/// <summary>
		/// If the word contains the Token
		/// </summary>
		Contains
	}

}
