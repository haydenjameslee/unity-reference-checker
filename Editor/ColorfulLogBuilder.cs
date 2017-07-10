using System.Text;

namespace UnityRefChecker
{
    public class ColorfulLogBuilder
    {
        private StringBuilder log = new StringBuilder();
        private bool colorful = true;

        public void SetColorful(bool use) {
            colorful = use;
        }

        public void StartColor() {
            if (colorful) {
                log.Append("<color=red>");
            }
        }

        public void EndColor() {
            if (colorful) {
                log.Append("</color>");
            }
        }

        public void Append(string text) {
            log.Append(text);
        }

        public override string ToString() {
            return log.ToString();
        }
    }
}
