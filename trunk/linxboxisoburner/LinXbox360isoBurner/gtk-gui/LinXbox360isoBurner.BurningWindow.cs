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
    
    
    public partial class BurningWindow {
        
        private Gtk.Fixed fixed_burn;
        
        private Gtk.Label label_status;
        
        private Gtk.Button button_cancel;
        
        protected virtual void Build() {
            Stetic.Gui.Initialize(this);
            // Widget LinXbox360isoBurner.BurningWindow
            this.Name = "LinXbox360isoBurner.BurningWindow";
            this.Title = "Burning...";
            this.Icon = Gdk.Pixbuf.LoadFromResource("icon.png");
            this.WindowPosition = ((Gtk.WindowPosition)(1));
            this.Resizable = false;
            this.AllowGrow = false;
            this.DestroyWithParent = true;
            this.Gravity = ((Gdk.Gravity)(5));
            this.SkipPagerHint = true;
            // Container child LinXbox360isoBurner.BurningWindow.Gtk.Container+ContainerChild
            this.fixed_burn = new Gtk.Fixed();
            this.fixed_burn.Name = "fixed_burn";
            this.fixed_burn.HasWindow = false;
            // Container child fixed_burn.Gtk.Fixed+FixedChild
            this.label_status = new Gtk.Label();
            this.label_status.Name = "label_status";
            this.label_status.Xpad = 96;
            this.label_status.Ypad = 5;
            this.label_status.Xalign = 1F;
            this.label_status.LabelProp = "...";
            this.label_status.Justify = ((Gtk.Justification)(2));
            this.label_status.SingleLineMode = true;
            this.fixed_burn.Add(this.label_status);
            Gtk.Fixed.FixedChild w1 = ((Gtk.Fixed.FixedChild)(this.fixed_burn[this.label_status]));
            w1.X = 1;
            w1.Y = 19;
            // Container child fixed_burn.Gtk.Fixed+FixedChild
            this.button_cancel = new Gtk.Button();
            this.button_cancel.CanFocus = true;
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.UseUnderline = true;
            this.button_cancel.Label = "Cancel";
            this.fixed_burn.Add(this.button_cancel);
            Gtk.Fixed.FixedChild w2 = ((Gtk.Fixed.FixedChild)(this.fixed_burn[this.button_cancel]));
            w2.X = 75;
            w2.Y = 53;
            this.Add(this.fixed_burn);
            if ((this.Child != null)) {
                this.Child.ShowAll();
            }
            this.DefaultWidth = 211;
            this.DefaultHeight = 124;
            this.Show();
            this.DeleteEvent += new Gtk.DeleteEventHandler(this.OnDeleteEvent);
            this.SizeAllocated += new Gtk.SizeAllocatedHandler(this.OnSizeAllocated);
            this.WindowStateEvent += new Gtk.WindowStateEventHandler(this.OnWindowStateEvent);
        }
    }
}
