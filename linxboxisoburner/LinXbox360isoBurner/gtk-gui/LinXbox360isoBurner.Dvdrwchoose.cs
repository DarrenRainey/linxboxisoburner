
// This file has been generated by the GUI designer. Do not modify.
namespace LinXbox360isoBurner
{
	public partial class Dvdrwchoose
	{
		private global::Gtk.Fixed @fixed;
		private global::Gtk.Label label;
		private global::Gtk.ComboBox combobox;
		private global::Gtk.Button button_ok;
        
		protected virtual void Build ()
		{
			global::Stetic.Gui.Initialize (this);
			// Widget LinXbox360isoBurner.Dvdrwchoose
			this.Name = "LinXbox360isoBurner.Dvdrwchoose";
			this.Title = "Choose your dvdrw device";
			this.WindowPosition = ((global::Gtk.WindowPosition)(4));
			this.Resizable = false;
			this.AllowGrow = false;
			// Internal child LinXbox360isoBurner.Dvdrwchoose.VBox
			global::Gtk.VBox w1 = this.VBox;
			w1.Name = "dialog_VBox";
			w1.BorderWidth = ((uint)(2));
			// Container child dialog_VBox.Gtk.Box+BoxChild
			this.@fixed = new global::Gtk.Fixed ();
			this.@fixed.Name = "fixed";
			this.@fixed.HasWindow = false;
			// Container child fixed.Gtk.Fixed+FixedChild
			this.label = new global::Gtk.Label ();
			this.label.Name = "label";
			this.label.LabelProp = "Choose your dvdrw device:";
			this.@fixed.Add (this.label);
			global::Gtk.Fixed.FixedChild w2 = ((global::Gtk.Fixed.FixedChild)(this.@fixed [this.label]));
			w2.X = 15;
			w2.Y = 9;
			// Container child fixed.Gtk.Fixed+FixedChild
			this.combobox = global::Gtk.ComboBox.NewText ();
			this.combobox.Name = "combobox";
			this.@fixed.Add (this.combobox);
			global::Gtk.Fixed.FixedChild w3 = ((global::Gtk.Fixed.FixedChild)(this.@fixed [this.combobox]));
			w3.X = 15;
			w3.Y = 31;
			w1.Add (this.@fixed);
			global::Gtk.Box.BoxChild w4 = ((global::Gtk.Box.BoxChild)(w1 [this.@fixed]));
			w4.Position = 0;
			// Internal child LinXbox360isoBurner.Dvdrwchoose.ActionArea
			global::Gtk.HButtonBox w5 = this.ActionArea;
			w5.Name = "dialog1_ActionArea";
			w5.Spacing = 6;
			w5.BorderWidth = ((uint)(5));
			w5.LayoutStyle = ((global::Gtk.ButtonBoxStyle)(2));
			// Container child dialog1_ActionArea.Gtk.ButtonBox+ButtonBoxChild
			this.button_ok = new global::Gtk.Button ();
			this.button_ok.CanDefault = true;
			this.button_ok.CanFocus = true;
			this.button_ok.Name = "button_ok";
			this.button_ok.UseUnderline = true;
			this.button_ok.Label = "OK";
			this.AddActionWidget (this.button_ok, -6);
			global::Gtk.ButtonBox.ButtonBoxChild w6 = ((global::Gtk.ButtonBox.ButtonBoxChild)(w5 [this.button_ok]));
			w6.Expand = false;
			w6.Fill = false;
			if ((this.Child != null)) {
				this.Child.ShowAll ();
			}
			this.DefaultWidth = 400;
			this.DefaultHeight = 139;
			this.Show ();
			this.button_ok.Clicked += new global::System.EventHandler (this.OnButtonOkClicked);
		}
	}
}
