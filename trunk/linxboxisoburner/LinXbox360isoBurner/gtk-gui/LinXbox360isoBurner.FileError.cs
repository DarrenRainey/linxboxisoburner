// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------

namespace LinXbox360isoBurner {
    
    
    public partial class FileError {
        
        private Gtk.Fixed @fixed;
        
        private Gtk.Button button;
        
        private Gtk.Label label_error;
        
        protected virtual void Build() {
            Stetic.Gui.Initialize(this);
            // Widget LinXbox360isoBurner.FileError
            this.Name = "LinXbox360isoBurner.FileError";
            this.Title = "FileError";
            this.Icon = Gdk.Pixbuf.LoadFromResource("icon.png");
            this.WindowPosition = ((Gtk.WindowPosition)(3));
            this.Resizable = false;
            this.AllowGrow = false;
            this.Gravity = ((Gdk.Gravity)(10));
            this.SkipPagerHint = true;
            this.SkipTaskbarHint = true;
            // Container child LinXbox360isoBurner.FileError.Gtk.Container+ContainerChild
            this.@fixed = new Gtk.Fixed();
            this.@fixed.Name = "fixed";
            this.@fixed.HasWindow = false;
            // Container child fixed.Gtk.Fixed+FixedChild
            this.button = new Gtk.Button();
            this.button.CanFocus = true;
            this.button.Name = "button";
            this.button.UseUnderline = true;
            this.button.Label = "Close";
            this.@fixed.Add(this.button);
            Gtk.Fixed.FixedChild w1 = ((Gtk.Fixed.FixedChild)(this.@fixed[this.button]));
            w1.X = 67;
            w1.Y = 41;
            // Container child fixed.Gtk.Fixed+FixedChild
            this.label_error = new Gtk.Label();
            this.label_error.Name = "label_error";
            this.label_error.Xpad = 22;
            this.label_error.LabelProp = ".dvd file error";
            this.label_error.Justify = ((Gtk.Justification)(2));
            this.@fixed.Add(this.label_error);
            Gtk.Fixed.FixedChild w2 = ((Gtk.Fixed.FixedChild)(this.@fixed[this.label_error]));
            w2.X = 28;
            w2.Y = 15;
            this.Add(this.@fixed);
            if ((this.Child != null)) {
                this.Child.ShowAll();
            }
            this.DefaultWidth = 201;
            this.DefaultHeight = 117;
            this.Show();
            this.button.Clicked += new System.EventHandler(this.OnButtonClicked);
        }
    }
}
