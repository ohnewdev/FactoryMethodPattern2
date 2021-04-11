using System;


/// <summary>
/// FactoryMethodPattern
/// 
/// 팩토리 메소드에서 템플릿 메소드의 사용됨을 안다.
/// 구조와 구현의 분리를 이해함.
/// 
/// 요구사항
/// 게임 아이템과 아이템 생성을 구현
///     - 아이템을 생성하기 전에 데이터 베이스에서 아이템 정보를 요청
///     - 복제방지를 위해 로그정보 남김
/// 아이템을 생성하는 주체를 intemCreator로 이름 짓다.
/// 아이템은 item 이라는 인터페이스로 다룰 수 있도록 합니다.
///     - item은 use 함수를 기본 함수로 갖고 있습니다.
/// 현재 아이템의 종류는 체력 회복 물약, 마력 회복 물약이 있습니다.
/// 
/// </summary>
namespace FactoryMethodPattern2
{
    class Program
    {
        static void Main(string[] args)
        {

            AbstItemCreator creator = new ItemCreator();
            Item item;

            item = creator.Create("HP");
            item.Use();

            item = creator.Create("MP");
            item.Use();

            Console.ReadKey();

        }
    }



    public abstract class AbstItemCreator
    {

        // 팩토리 메소드 -> 템플릿 메소드
        public Item Create(string type)
        {
            Item item;


            //step1
            requestItemInfo(type);
            //step2
            item = createItem(type);
            //step3
            createItemLog(type);
            return item;
        }

        abstract protected void requestItemInfo(string type);
        abstract protected void createItemLog(string type);

        abstract protected Item createItem(string type);
    }


    public interface Item
    {
        public void Use();
    }

    public class ItemCreator : AbstItemCreator
    {
        protected override Item createItem(string type)
        {
            switch (type)
            {
                case "MP":
                    return new MpPotion();
                case "HP":
                    return new HpPotion();

            }
            return null;

        }

        protected override void createItemLog(string type)
        {
            Console.WriteLine(type + "물약 만들었습니다. " + DateTime.Now);
        }

        protected override void requestItemInfo(string type)
        {
            Console.WriteLine($"데이터베이스에서 {type}물약 정보를 가져옵니다. {DateTime.Now}");
        }
    }
    public class HpPotion : Item
    {
        public void Use()
        {
            Console.WriteLine("체력을 회복하였습니다. ");
        }
    }

    public class MpPotion : Item
    {
        public void Use()
        {
            Console.WriteLine("마력을 회복하였습니다");
        }
    }

}
