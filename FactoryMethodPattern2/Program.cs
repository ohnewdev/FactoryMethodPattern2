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

            ItemCreator creator;
            Item item;
            creator = new HpCreator();
            item = creator.Create();
            item.Use();

            creator = new MpCreator();
            item = creator.Create();
            item.Use();

            Console.ReadKey();

        }
    }



    public abstract class ItemCreator
    {

        // 팩토리 메소드 -> 템플릿 메소드
        public Item Create()
        {
            Item item;


            //step1
            requestItemInfo();
            //step2
            item = createItem();
            //step3
            createItemLog();
            return item;
        }

        abstract protected void requestItemInfo();
        abstract protected void createItemLog();

        abstract protected Item createItem();
    }


    public interface Item
    {
        public void Use();
    }


    public class HpPotion : Item
    {
        public void Use()
        {
            Console.WriteLine("체력회복");
        }
    }

    public class MpPotion : Item
    {
        public void Use()
        {
            Console.WriteLine("마력회복");
        }
    }


    // 위에 2가지를 생성하기 위해. 

    public class HpCreator : ItemCreator
    {
        protected override Item createItem()
        {
            Console.WriteLine("체력 물약 생성....");
            return new HpPotion();

        }

        protected override void createItemLog()
        {
            Console.WriteLine("체력물약 만들었습니다. " + DateTime.Now);
        }

        protected override void requestItemInfo()
        {
            Console.WriteLine("데이터베이스에서 체력물약 정보를 가져옵니다. " + DateTime.Now);
        }


    }
    public class MpCreator : ItemCreator
    {
        protected override Item createItem()
        {
            Console.WriteLine("마력 물약 생성....");
            return new MpPotion();

        }

        protected override void createItemLog()
        {
            Console.WriteLine("마력물약 만들었습니다. " + DateTime.Now);
        }

        protected override void requestItemInfo()
        {
            Console.WriteLine("데이터베이스에서 마력물약 정보를 가져옵니다. " + DateTime.Now);
        }
    }
}
