using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Nethereum.ABI.Decoders;
using Nethereum.ABI.FunctionEncoding;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.Util;
using Xunit;

namespace Nethereum.ABI.UnitTests
{
    public class AttributeStructAbiArrayDecodingTest
    {
        public partial class PurchaseOrder : PurchaseOrderBase { }

        public class PurchaseOrderBase
        {
            [Parameter("uint256", "id", 1)]
            public virtual BigInteger Id { get; set; }
            [Parameter("tuple[]", "lineItem", 2)]
            public virtual List<LineItem> LineItem { get; set; }
            [Parameter("uint256", "customerId", 3)]
            public virtual BigInteger CustomerId { get; set; }
        }

        public partial class LineItem : LineItemBase { }

        public class LineItemBase
        {
            [Parameter("uint256", "id", 1)]
            public virtual BigInteger Id { get; set; }
            [Parameter("uint256", "productId", 2)]
            public virtual BigInteger ProductId { get; set; }
            [Parameter("uint256", "quantity", 3)]
            public virtual BigInteger Quantity { get; set; }
            [Parameter("string", "description", 4)]
            public virtual string Description { get; set; }
        }

        [Fact]
        public void ShouldDecodeStructArrayTest4()
        {
            var encoded = "0x000000000000000000000000000000000000000000000000000000000000000100000000000000000000000000000000000000000000000000000000000000200000000000000000000000000000000000000000000000000000000000000001000000000000000000000000000000000000000000000000000000000000006000000000000000000000000000000000000000000000000000000000000003e800000000000000000000000000000000000000000000000000000000000000030000000000000000000000000000000000000000000000000000000000000060000000000000000000000000000000000000000000000000000000000000012000000000000000000000000000000000000000000000000000000000000001e00000000000000000000000000000000000000000000000000000000000000001000000000000000000000000000000000000000000000000000000000000006400000000000000000000000000000000000000000000000000000000000000020000000000000000000000000000000000000000000000000000000000000080000000000000000000000000000000000000000000000000000000000000000668656c6c6f310000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000200000000000000000000000000000000000000000000000000000000000000c800000000000000000000000000000000000000000000000000000000000000030000000000000000000000000000000000000000000000000000000000000080000000000000000000000000000000000000000000000000000000000000000668656c6c6f3200000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000003000000000000000000000000000000000000000000000000000000000000012c00000000000000000000000000000000000000000000000000000000000000040000000000000000000000000000000000000000000000000000000000000080000000000000000000000000000000000000000000000000000000000000000668656c6c6f330000000000000000000000000000000000000000000000000000";
            var arrayType = new DynamicArrayType("tuple[]");

            var resultArray = arrayType.Decode<List<PurchaseOrder>>(encoded.HexToByteArray());

            var result = resultArray[0];

            Assert.Equal(1, result.Id);
            Assert.Equal(1000, result.CustomerId);
            Assert.Equal(1, result.LineItem[0].Id);
            Assert.Equal(100, result.LineItem[0].ProductId);
            Assert.Equal(2, result.LineItem[0].Quantity);
            Assert.Equal(2, result.LineItem[1].Id);
            Assert.Equal(200, result.LineItem[1].ProductId);
            Assert.Equal(3, result.LineItem[1].Quantity);
        }


        [Fact]
        public void ShouldDecodeStructArrayTest3()
        {
            var encoded = "0x000000000000000000000000000000000000000000000000000000000000000100000000000000000000000000000000000000000000000000000000000000200000000000000000000000000000000000000000000000000000000000000001000000000000000000000000000000000000000000000000000000000000006000000000000000000000000000000000000000000000000000000000000003e800000000000000000000000000000000000000000000000000000000000000030000000000000000000000000000000000000000000000000000000000000060000000000000000000000000000000000000000000000000000000000000012000000000000000000000000000000000000000000000000000000000000001e00000000000000000000000000000000000000000000000000000000000000001000000000000000000000000000000000000000000000000000000000000006400000000000000000000000000000000000000000000000000000000000000020000000000000000000000000000000000000000000000000000000000000080000000000000000000000000000000000000000000000000000000000000000668656c6c6f310000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000200000000000000000000000000000000000000000000000000000000000000c800000000000000000000000000000000000000000000000000000000000000030000000000000000000000000000000000000000000000000000000000000080000000000000000000000000000000000000000000000000000000000000000668656c6c6f3200000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000003000000000000000000000000000000000000000000000000000000000000012c00000000000000000000000000000000000000000000000000000000000000040000000000000000000000000000000000000000000000000000000000000080000000000000000000000000000000000000000000000000000000000000000668656c6c6f330000000000000000000000000000000000000000000000000000";
            var arrayType = new DynamicArrayType("tuple[]");

            var resultArray = (List<PurchaseOrder>)(arrayType.Decode(encoded.HexToByteArray(), typeof(List<PurchaseOrder>)));

            var result = resultArray[0];

            Assert.Equal(1, result.Id);
            Assert.Equal(1000, result.CustomerId);
            Assert.Equal(1, result.LineItem[0].Id);
            Assert.Equal(100, result.LineItem[0].ProductId);
            Assert.Equal(2, result.LineItem[0].Quantity);
            Assert.Equal(2, result.LineItem[1].Id);
            Assert.Equal(200, result.LineItem[1].ProductId);
            Assert.Equal(3, result.LineItem[1].Quantity);
        }

    }


    public class StructAbiAttributeDecodingTest
    {
        [Fact]
        public void ShouldDecodeStructUsingAttributes()
        {
            var purchaseOrder = new PurchaseOrder();
            purchaseOrder.CustomerId = 1000;
            purchaseOrder.Id = 1;
            purchaseOrder.LineItem = new List<LineItem>();
            purchaseOrder.LineItem.Add(new LineItem() { Id = 1, ProductId = 100, Quantity = 2, Description = "hello1" });
            purchaseOrder.LineItem.Add(new LineItem() { Id = 2, ProductId = 200, Quantity = 3, Description = "hello2" });
            purchaseOrder.LineItem.Add(new LineItem() { Id = 3, ProductId = 300, Quantity = 4, Description = "hello3" });

            var encoded = "0000000000000000000000000000000000000000000000000000000000000001000000000000000000000000000000000000000000000000000000000000006000000000000000000000000000000000000000000000000000000000000003e80000000000000000000000000000000000000000000000000000000000000060000000000000000000000000000000000000000000000000000000000000012000000000000000000000000000000000000000000000000000000000000001e00000000000000000000000000000000000000000000000000000000000000001000000000000000000000000000000000000000000000000000000000000006400000000000000000000000000000000000000000000000000000000000000020000000000000000000000000000000000000000000000000000000000000080000000000000000000000000000000000000000000000000000000000000000668656c6c6f310000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000200000000000000000000000000000000000000000000000000000000000000c800000000000000000000000000000000000000000000000000000000000000030000000000000000000000000000000000000000000000000000000000000080000000000000000000000000000000000000000000000000000000000000000668656c6c6f3200000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000003000000000000000000000000000000000000000000000000000000000000012c00000000000000000000000000000000000000000000000000000000000000040000000000000000000000000000000000000000000000000000000000000080000000000000000000000000000000000000000000000000000000000000000668656c6c6f330000000000000000000000000000000000000000000000000000";

            var tupleType = new TupleType();
            var result = tupleType.Decode<PurchaseOrder>(encoded.HexToByteArray());

            Assert.Equal(1, result.Id);
            Assert.Equal(1000, result.CustomerId);
            Assert.Equal(1, result.LineItem[0].Id);
            Assert.Equal(100, result.LineItem[0].ProductId);
            Assert.Equal(2, result.LineItem[0].Quantity);
            Assert.Equal(2, result.LineItem[1].Id);
            Assert.Equal(200, result.LineItem[1].ProductId);
            Assert.Equal(3, result.LineItem[1].Quantity);

        }


        public partial class PurchaseOrder : PurchaseOrderBase { }

        public class PurchaseOrderBase
        {
            [Parameter("uint256", "id", 1)]
            public virtual BigInteger Id { get; set; }
            [Parameter("tuple[3]", "lineItem", 2)]
            public virtual List<LineItem> LineItem { get; set; }
            [Parameter("uint256", "customerId", 3)]
            public virtual BigInteger CustomerId { get; set; }
        }

        public partial class LineItem : LineItemBase { }

        public class LineItemBase
        {
            [Parameter("uint256", "id", 1)]
            public virtual BigInteger Id { get; set; }
            [Parameter("uint256", "productId", 2)]
            public virtual BigInteger ProductId { get; set; }
            [Parameter("uint256", "quantity", 3)]
            public virtual BigInteger Quantity { get; set; }
            [Parameter("string", "description", 4)]
            public virtual string Description { get; set; }
        }

    }


    public class StaticArrayEncodingTests
    {
        

        [Fact]
        public virtual void ShouldDecodeStaticIntArray()
        {
            //Given

            var given =
                "000000000000000000000000000000000000000000000000000000000003944700000000000000000000000000000000000000000000000000000000000394480000000000000000000000000000000000000000000000000000000000039449000000000000000000000000000000000000000000000000000000000003944a000000000000000000000000000000000000000000000000000000000003944b000000000000000000000000000000000000000000000000000000000003944c000000000000000000000000000000000000000000000000000000000003944d000000000000000000000000000000000000000000000000000000000003944e000000000000000000000000000000000000000000000000000000000003944f0000000000000000000000000000000000000000000000000000000000039450000000000000000000000000000000000000000000000000000000000003945100000000000000000000000000000000000000000000000000000000000394520000000000000000000000000000000000000000000000000000000000039453000000000000000000000000000000000000000000000000000000000003945400000000000000000000000000000000000000000000000000000000000394550000000000000000000000000000000000000000000000000000000000039456000000000000000000000000000000000000000000000000000000000003945700000000000000000000000000000000000000000000000000000000000394580000000000000000000000000000000000000000000000000000000000039459000000000000000000000000000000000000000000000000000000000003945a";

            var arrayType = ArrayType.CreateABIType("uint[20]");

            //when
            var list = arrayType.Decode<List<BigInteger>>(given);

            //then


            if (list != null)
            {
                Assert.Equal(20, list.Count);

                for (var i = 0; i < list.Count; i++)
                    Assert.Equal(new BigInteger(i + 234567), list[i]);
            }
            else
            {
                throw new Exception("Expected to return IList object when decoding array");
            }
        }

        [Fact]
        public virtual void ShouldDecodeMultidimensionAddressArray()
        {
            //Given

            var given =
                "000000000000000000000000000000000000000000000000000000000000000a000000000000000000000000a0b86991c6218b36c1d19d4a2e9eb0ce3606eb48000000000000000000000000c02aaa39b223fe8d0a0e5c4f27ead9083c756cc2000000000000000000000000b4e16d0168e52d35cacd2c6185b44281ec28c9dc0000000000000000000000008e870d67f660d95d5be530380d0ec0bd388289e1000000000000000000000000a0b86991c6218b36c1d19d4a2e9eb0ce3606eb480000000000000000000000003139ffc91b99aa94da8a2dc13f1fc36f9bdc98ee00000000000000000000000006af07097c9eeb7fd685c692751d5c66db49c215000000000000000000000000c02aaa39b223fe8d0a0e5c4f27ead9083c756cc200000000000000000000000012ede161c702d1494612d19f05992f43aa6a26fb0000000000000000000000006b175474e89094c44da98b954eedeac495271d0f000000000000000000000000c02aaa39b223fe8d0a0e5c4f27ead9083c756cc2000000000000000000000000a478c2975ab1ea89e8196811f51a7b7ade33eb11000000000000000000000000408e41876cccdc0f92210600ef50372656052a38000000000000000000000000a0b86991c6218b36c1d19d4a2e9eb0ce3606eb4800000000000000000000000007f068ca326a469fc1d87d85d448990c8cba7df90000000000000000000000006b175474e89094c44da98b954eedeac495271d0f000000000000000000000000a0b86991c6218b36c1d19d4a2e9eb0ce3606eb48000000000000000000000000ae461ca67b15dc8dc81ce7615e0320da1a9ab8d5000000000000000000000000c02aaa39b223fe8d0a0e5c4f27ead9083c756cc2000000000000000000000000fa3e941d1f6b7b10ed84a0c211bfa8aee907965e000000000000000000000000ce407cd7b95b39d3b4d53065e711e713dd5c59990000000000000000000000001f573d6fb3f13d689ff844b4ce37794d79a7ff1c0000000000000000000000006b175474e89094c44da98b954eedeac495271d0f00000000000000000000000033c2d48bc95fb7d0199c5c693e7a9f527145a9af0000000000000000000000000d8775f648430679a709e98d2b0cb6250d2887ef000000000000000000000000c02aaa39b223fe8d0a0e5c4f27ead9083c756cc2000000000000000000000000b6909b960dbbe7392d405429eb2b3649752b483800000000000000000000000039aa39c021dfbae8fac545936693ac917d5e75630000000000000000000000005d3a536e4d6dbd6114cc1ead35777bab948e364300000000000000000000000030eb5e15476e6a80f4f3cd8479749b4881dab1b8";

            var arrayType = ArrayType.CreateABIType("address[3][]");

            //when
            var list = arrayType.Decode<List<List<string>>>(given);

            //then


            if (list != null)
            {
                Assert.Equal(10, list.Count);

                for (var i = 0; i < list.Count; i++)
                {
                    Assert.Equal(list[i].Count, 3);
                    foreach (var address in list[i])
                    {
                        Assert.True(address.IsValidEthereumAddressHexFormat());
                    }
                }
                ;
            }
            else
            {
                throw new Exception("Expected to return IList object when decoding array");
            }
        }

        [Fact]
        public virtual void ShouldEncodeStaticIntArray()
        {
            //Given
            var array = new uint[20];
            for (uint i = 0; i < 20; i++)
                array[i] = i + 234567;

            var arrayType = ArrayType.CreateABIType("uint[20]");

            //when
            var result = arrayType.Encode(array).ToHex();

            //then
            var expected =
                "000000000000000000000000000000000000000000000000000000000003944700000000000000000000000000000000000000000000000000000000000394480000000000000000000000000000000000000000000000000000000000039449000000000000000000000000000000000000000000000000000000000003944a000000000000000000000000000000000000000000000000000000000003944b000000000000000000000000000000000000000000000000000000000003944c000000000000000000000000000000000000000000000000000000000003944d000000000000000000000000000000000000000000000000000000000003944e000000000000000000000000000000000000000000000000000000000003944f0000000000000000000000000000000000000000000000000000000000039450000000000000000000000000000000000000000000000000000000000003945100000000000000000000000000000000000000000000000000000000000394520000000000000000000000000000000000000000000000000000000000039453000000000000000000000000000000000000000000000000000000000003945400000000000000000000000000000000000000000000000000000000000394550000000000000000000000000000000000000000000000000000000000039456000000000000000000000000000000000000000000000000000000000003945700000000000000000000000000000000000000000000000000000000000394580000000000000000000000000000000000000000000000000000000000039459000000000000000000000000000000000000000000000000000000000003945a";

            Assert.Equal(expected, result);
        }



        
    }
}