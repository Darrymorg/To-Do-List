
namespace To_Do_List
{
    internal class MyTask
    {
        public string Name { get; set; }
        public bool IsCompleted { get; set; }
        public MyTask(string name)
        {
            Name = name;
        }
        public override string ToString()
        {
            return Name;
        }
    }
}
