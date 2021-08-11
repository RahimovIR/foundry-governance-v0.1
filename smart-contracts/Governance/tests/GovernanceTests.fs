module GovernanceTests

open System
open System.Numerics
open Xunit
open FsUnit.Xunit
open FsUnit.CustomMatchers
open Constants
open TestBase
open GovernanceTestBase
open Nethereum.Web3
open Nethereum.Hex.HexConvertors.Extensions
open Nethereum.Web3.Accounts
open Nethereum.RPC.Eth.DTOs
open Nethereum.Contracts
open SolidityTypes
open AbiTypeProvider.Common

type System.String with
   member s1.icompare(s2: string) =
     System.String.Equals(s1, s2, StringComparison.CurrentCultureIgnoreCase);


//---------Test Testing------------

[<Specification("gFry", "constructor", 0)>]
[<Fact>]
let ``initializes with correct initial supply`` () =
    restore ()

    let gFryCon = getGFryContract()

    let totSup1 = gFryCon.totalSupplyQuery()
    let balance1 = gFryCon.balanceOfQuery(hardhatAccount)
    printfn "%O" totSup1
    // printf "--Mint 100--\n"
    // printf "Balance:\n"
    // printfn "%O" balance1
    // let mintTX = gFryCon.mint(hardhatAccount, "0x64")
    // printf "--Total supply--\n"
    // let totSup2 = gFryCon.totalSupplyQuery()
    // printfn "%O" totSup2
    // printf "--Burn 50--\n"
    // let burnTX = gFryCon.burn("0x32")
    // printf "--Total supply--\n"
    // let totSup3 = gFryCon.totalSupplyQuery()
    // printfn "%O" totSup3
    // let balance2 = gFryCon.balanceOfQuery(hardhatAccount)
    // printf "Balance:\n"
    // printfn "%O" balance2
    // printf "----------------\n"

    let zero = bigint 0;
    should equal zero (gFryCon.totalSupplyQuery())
    // let authority, contract = getDEthContractAndAuthority()

    // // check the rights
    // let functionName = Web3.Sha3("changeSettings(uint256,uint256,uint256)").Substring(0, 8).HexToByteArray()
    // let canCall = authority.canCallQuery (foundryTreasury, contract.Address, functionName)

    // // check the balance of initialRecipient
    // let balanceOfInitialRecipient = contract.balanceOfQuery(initialRecipient)

    // shouldEqualIgnoringCase gulper <| contract.gulperQuery()
    // shouldEqualIgnoringCase proxyCache <| contract.cacheQuery()
    // should equal cdpId <| contract.cdpIdQuery()
    // shouldEqualIgnoringCase makerManager <| contract.makerManagerQuery()
    // shouldEqualIgnoringCase ethGemJoin <| contract.ethGemJoinQuery()
    // shouldEqualIgnoringCase saverProxy <| contract.saverProxyQuery()
    // shouldEqualIgnoringCase saverProxyActions <| contract.saverProxyActionsQuery()
    // shouldEqualIgnoringCase oracleContractMainnet.Address <| contract.oracleQuery()
    // should be True canCall
    // should greaterThan BigInteger.Zero balanceOfInitialRecipient
    // dEthContract.minRedemptionRatioQuery() |> should equal <| (bigint 160) * ratio

//---------End Test Testing------------


// [<Specification("dEth", "constructor", 0)>]
// [<Fact>]
// let ``initializes with correct values and rights assigned`` () =
//     restore ()

//     let authority, contract = getDEthContractAndAuthority()

//     // check the rights
//     let functionName = Web3.Sha3("changeSettings(uint256,uint256,uint256)").Substring(0, 8).HexToByteArray()
//     let canCall = authority.canCallQuery (foundryTreasury, contract.Address, functionName)

//     // check the balance of initialRecipient
//     let balanceOfInitialRecipient = contract.balanceOfQuery(initialRecipient)

//     shouldEqualIgnoringCase gulper <| contract.gulperQuery()
//     shouldEqualIgnoringCase proxyCache <| contract.cacheQuery()
//     should equal cdpId <| contract.cdpIdQuery()
//     shouldEqualIgnoringCase makerManager <| contract.makerManagerQuery()
//     shouldEqualIgnoringCase ethGemJoin <| contract.ethGemJoinQuery()
//     shouldEqualIgnoringCase saverProxy <| contract.saverProxyQuery()
//     shouldEqualIgnoringCase saverProxyActions <| contract.saverProxyActionsQuery()
//     shouldEqualIgnoringCase oracleContractMainnet.Address <| contract.oracleQuery()
//     should be True canCall
//     should greaterThan BigInteger.Zero balanceOfInitialRecipient
//     dEthContract.minRedemptionRatioQuery() |> should equal <| (bigint 160) * ratio

// [<Specification("Oracle", "constructor", 0)>]
// [<Fact>]
// let ``inits to provided parameters`` () =
//     restore ()

//     let (makerOracle, daiUsdOracle, ethUsdOracle) = (makeAccount().Address, makeAccount().Address, makeAccount().Address)
//     let contract = Contracts.OracleContract(ethConn.GetWeb3, makerOracle, daiUsdOracle, ethUsdOracle)

//     shouldEqualIgnoringCase makerOracle (contract.makerOracleQuery ())
//     shouldEqualIgnoringCase daiUsdOracle (contract.daiUsdOracleQuery ())
//     shouldEqualIgnoringCase ethUsdOracle (contract.ethUsdOracleQuery ())

// [<Specification("Oracle", "getEthDaiPrice", 0)>]
// [<Theory>]
// [<InlineData(0.08)>]
// [<InlineData(0.1)>]
// [<InlineData(0.12)>]
// let ``price is correct given source prices within ten percents of one another`` differencePercent =
//     restore ()

//     let (priceMaker, _, priceNonMakerDaiEth, _) = initOraclesDefault differencePercent

//     let price = oracleContract.getEthDaiPriceQueryAsync() |> runNow

//     let expected =
//         if differencePercent <= 0.1M
//         then 
//             toMakerPriceFormatDecimal priceNonMakerDaiEth
//         else 
//             toMakerPriceFormatDecimal priceMaker

//     should equal expected price

// [<Specification("dEth", "constructor", 0)>]
// [<Fact>]
// let ``initializes with correct values and rights assigned`` () =
//     restore ()

//     let authority, contract = getDEthContractAndAuthority()

//     // check the rights
//     let functionName = Web3.Sha3("changeSettings(uint256,uint256,uint256)").Substring(0, 8).HexToByteArray()
//     let canCall = authority.canCallQuery (foundryTreasury, contract.Address, functionName)

//     // check the balance of initialRecipient
//     let balanceOfInitialRecipient = contract.balanceOfQuery(initialRecipient)

//     shouldEqualIgnoringCase gulper <| contract.gulperQuery()
//     shouldEqualIgnoringCase proxyCache <| contract.cacheQuery()
//     should equal cdpId <| contract.cdpIdQuery()
//     shouldEqualIgnoringCase makerManager <| contract.makerManagerQuery()
//     shouldEqualIgnoringCase ethGemJoin <| contract.ethGemJoinQuery()
//     shouldEqualIgnoringCase saverProxy <| contract.saverProxyQuery()
//     shouldEqualIgnoringCase saverProxyActions <| contract.saverProxyActionsQuery()
//     shouldEqualIgnoringCase oracleContractMainnet.Address <| contract.oracleQuery()
//     should be True canCall
//     should greaterThan BigInteger.Zero balanceOfInitialRecipient
//     dEthContract.minRedemptionRatioQuery() |> should equal <| (bigint 160) * ratio

// [<Specification("dEth", "changeGulper", 0)>]
// [<Fact>]
// let ``can be changed by owner`` () =
//     restore ()
//     let contract = getDEthContract ()
//     let randomAddress = makeAccount().Address
//     contract.changeGulper(randomAddress) |> ignore
//     shouldEqualIgnoringCase randomAddress <| contract.gulperQuery()

// [<Specification("dEth", "changeGulper", 1)>]
// [<Fact>]
// let ``cannot be changed by non-owner`` () = 
//     restore ()
//     let contract = getDEthContract ()
//     let account = Account(hardhatPrivKey2)
//     let oldGulper = contract.gulperQuery()

//     let debug = Debug(EthereumConnection(hardhatURI, account.PrivateKey))
//     let data = contract.changeGulperData(account.Address)
//     let receipt = debug.Forward(contract.Address,  data)
//     let forwardEvent = debug.DecodeForwardedEvents receipt |> Seq.head
//     forwardEvent |> shouldRevertWithUnknownMessage
//     shouldEqualIgnoringCase oldGulper <| contract.gulperQuery()

// let giveCDPToDSProxyTestBase shouldThrow = 
//     restore ()
//     let newContract = getDEthContract ()

//     let executeGiveCDPFromPrivateKey shouldThrow =
//         if shouldThrow then 
//             let debug = Debug(EthereumConnection(hardhatURI, hardhatPrivKey2))
//             let data = dEthContract.giveCDPToDSProxyData(newContract.Address)
//             debug.Forward(newContract.Address, data)
//         else 
//             dEthContract.giveCDPToDSProxy(newContract.Address)
    
//     let giveCDPToDSProxyReceipt = executeGiveCDPFromPrivateKey shouldThrow

//     if shouldThrow then
//         let forwardEvent = debug.DecodeForwardedEvents giveCDPToDSProxyReceipt |> Seq.head
//         forwardEvent |> shouldRevertWithUnknownMessage
//     else
//         giveCDPToDSProxyReceipt.Succeeded () |> should equal true
//         dEthContract.riskLimitQuery() |> should equal (BigInteger 0)


// [<Specification("dEth", "giveCDPToDSProxy", 0)>]
// [<Fact>]
// let ``dEth - giveCDPToDSProxy - can be called by owner`` () = giveCDPToDSProxyTestBase false

// [<Specification("dEth", "giveCDPToDSProxy", 1)>]
// [<Fact>]
// let ``dEth - giveCDPToDSProxy - cannot be called by non-owner`` () = giveCDPToDSProxyTestBase true

// [<Specification("dEth", "getCollateral", 0)>]
// [<Fact>]
// let ``dEth - getCollateral - returns similar values as those directly retrieved from the underlying contracts and calculated in F#`` () = 
//     restore ()
//     let contract = getDEthContract ()

//     let getCollateralOutput = contract.getCollateralQuery()
//     let (_, priceRay, _, cdpDetailedInfoOutput, collateralDenominatedDebt, excessCollateral) = 
//         getManuallyComputedCollateralValues oracleContractMainnet saverProxy cdpId
    
//     should equal priceRay getCollateralOutput._priceRAY
//     should equal cdpDetailedInfoOutput.collateral getCollateralOutput._totalCollateral
//     should equal cdpDetailedInfoOutput.debt getCollateralOutput._debt
//     should equal collateralDenominatedDebt getCollateralOutput._collateralDenominatedDebt
//     should equal excessCollateral getCollateralOutput._excessCollateral

// [<Specification("dEth", "getCollateralPriceRAY", 0)>]
// [<Fact>]
// let ``dEth - getCollateralPriceRAY - returns similar values as those directly retrieved from the underlying contracts and calculated in F#`` () = 
//     restore ()
//     let contract = getDEthContract ()

//     let ethDaiPrice = oracleContractMainnet.getEthDaiPriceQuery()
//     let expectedRay = BigInteger.Pow(bigint 10, 9) * ethDaiPrice

//     let actualRay = contract.getCollateralPriceRAYQuery()
//     should equal expectedRay actualRay

// [<Specification("dEth", "getExcessCollateral", 0)>]
// [<Fact>]
// let ``dEth - getExcessCollateral - returns similar values as those directly retrieved from the underlying contracts and calculated in F#`` () =
//     restore ()
//     let contract = getDEthContract ()

//     let (_, _, _, _, _, excessCollateral) = getManuallyComputedCollateralValues oracleContractMainnet saverProxy cdpId

//     let actual = contract.getExcessCollateralQuery()
//     should equal excessCollateral actual

// [<Specification("dEth", "getRatio", 0)>]
// [<Fact>]
// let ``dEth - getRatio - returns similar values as those directly retrieved from the underlying contracts and calculated in F#`` () =
//     restore ()
//     let contract = getDEthContract ()
//     let saverProxyContract = Contracts.MCDSaverProxyContract(saverProxy, ethConn.GetWeb3)
//     let manager = Contracts.ManagerLikeContract(makerManager, ethConn.GetWeb3)

//     let ilk = manager.ilksQuery(cdpId)
//     let price = saverProxyContract.getPriceQuery (ilk)
//     let getCdpInfoOutputDTO = saverProxyContract.getCdpInfoQuery(manager.Address,cdpId,ilk)

//     let expected = 
//         if getCdpInfoOutputDTO.Prop1 = BigInteger.Zero 
//         then 
//             BigInteger.Zero 
//         else 
//             rdiv (wmul getCdpInfoOutputDTO.Prop0 price) getCdpInfoOutputDTO.Prop1

//     let actual = contract.getRatioQuery()

//     should equal expected actual

// [<Specification("dEth", "changeSettings", 0)>]
// [<Theory>]
// [<InlineData(foundryTreasury, 180, 220, 220, 1, 1, 1)>]
// [<InlineData(ownerArg, 180, 220, 220, 1, 1, 1)>]
// [<InlineData(contractArg, 180, 220, 220, 1, 1, 1)>]
// let ``dEth - changeSettings - an authorised address can change the settings`` (addressArgument:string) (repaymentRatioExpected:int) (targetRatioExpected:int) (boostRatioExpected:int) (minRedemptionRatioExpected:int) (automationFeePercExpected:int) (riskLimitExpected:int) =
//     restore ()

//     let changeSettingsTxr = 
//         Contracts.dEthContract.changeSettingsFunction(
//             _minRedemptionRatio = bigint minRedemptionRatioExpected,
//             _automationFeePerc = bigint automationFeePercExpected, 
//             _riskLimit = bigint riskLimitExpected)
//         |> ethConn.MakeImpersonatedCallWithNoEther (mapInlineDataArgumentToAddress addressArgument dEthContract.Address) dEthContract.Address

//     changeSettingsTxr |> shouldSucceed

//     dEthContract.minRedemptionRatioQuery() |> should equal <| (bigint minRedemptionRatioExpected) * ratio
//     dEthContract.automationFeePercQuery() |> should equal (bigint automationFeePercExpected)
//     dEthContract.riskLimitQuery() |> should equal (bigint riskLimitExpected)

//     let event = changeSettingsTxr.DecodeAllEvents<Contracts.dEthContract.SettingsChangedEventDTO>() |> Seq.map (fun i -> i.Event) |> Seq.head
//     event._minRedemptionRatio |> should equal <| (bigint minRedemptionRatioExpected) * ratio
//     event._automationFeePerc |> should equal <| bigint automationFeePercExpected
//     event._riskLimit |> should equal <| bigint riskLimitExpected

// [<Specification("dEth", "changeSettings", 1)>]
// [<Theory>]
// [<InlineData(repaymentRatio, targetRatio, boostRatio, 1, 1, 1)>]
// let ``dEth - changeSettings - an unauthorised address cannot change the automation settings`` (repaymentRatioExpected:int) (targetRatioExpected:int) (boostRatioExpected:int) (minRedemptionRatioExpected:int) (automationFeePercExpected:int) (riskLimitExpected:int) = 
//     restore ()

//     let debug = Debug(EthereumConnection(hardhatURI, makeAccountWithBalance().PrivateKey))
//     let data = dEthContract.changeSettingsData(bigint minRedemptionRatioExpected, bigint automationFeePercExpected, bigint riskLimitExpected)

//     debug.Forward(dEthContract.Address, data)
//     |> debug.DecodeForwardedEvents
//     |> Seq.head
//     |> shouldRevertWithUnknownMessage // To clarify : We get no message because the auth code reverts without providing one

// [<Specification("dEth", "redeem", 0)>]
// [<Theory>]
// [<InlineData(10.0, 7.0, false)>]
// [<InlineData(1.0, 0.7, false)>]
// [<InlineData(0.01, 0.005, false)>]
// [<InlineData(0.001, 0.0005, false)>]
// [<InlineData(10.0, 7.0, true)>]
// [<InlineData(1.0, 0.05, true)>]
// let ``blah dEth - redeem - someone with a positive balance of dEth can redeem the expected amount of Ether`` (tokensToMint:float) (tokensToRedeem:float) (riskLevelShouldBeExceeded:bool) =
//     restore ()

//     let redeemerConnection = EthereumConnection(hardhatURI, hardhatPrivKey2)

//     let tokensToTransferBigInt = tokensToMint |> toE18
//     let tokensToRedeemBigInt = tokensToRedeem |> toE18

//     tokensToRedeemBigInt |> should lessThan tokensToTransferBigInt

//     dEthContract.transfer(redeemerConnection.Account.Address,tokensToTransferBigInt) |> shouldSucceed

//     let tokenBalanceBefore = balanceOf dEthContract redeemerConnection.Account.Address

//     let gulperBalanceBefore = getGulperEthBalance ()

//     if riskLevelShouldBeExceeded then
//         makeRiskLimitLessThanExcessCollateral dEthContract |> shouldSucceed

//     let receiverAddress = makeAccount().Address
    
//     let (protocolFeeExpected, automationFeeExpected, collateralRedeemedExpected, collateralReturnedExpected) = 
//         queryStateAndCalculateRedemptionValue dEthContract tokensToRedeemBigInt

//     let redeemerContract = Contracts.dEthContract(dEthContract.Address, redeemerConnection.GetWeb3)
//     let redeemTx = redeemerContract.redeem(receiverAddress,tokensToRedeemBigInt)
//     redeemTx |> shouldSucceed

//     receiverAddress |> ethConn.GetEtherBalance |> should equal collateralReturnedExpected
//     getGulperEthBalance () |> should equal (protocolFeeExpected + gulperBalanceBefore)

//     balanceOf dEthContract redeemerConnection.Account.Address |> should equal (tokenBalanceBefore - tokensToRedeemBigInt)

//     let event = redeemTx.DecodeAllEvents<Contracts.dEthContract.RedeemedEventDTO>() |> Seq.map (fun i -> i.Event) |> Seq.head
//     event._redeemer |> shouldEqualIgnoringCase redeemerConnection.Account.Address
//     event._receiver |> shouldEqualIgnoringCase receiverAddress
//     event._tokensRedeemed |> should equal tokensToRedeemBigInt
//     event._protocolFee |> should equal protocolFeeExpected
//     event._automationFee |> should equal automationFeeExpected
//     event._collateralRedeemed |> should equal collateralRedeemedExpected
//     event._collateralReturned |> should equal collateralReturnedExpected

// [<Specification("dEth", "redeem", 1)>]
// [<Theory>]
// [<InlineData(10000)>]
// let ``dEth - redeem - someone without a balance can never redeem Ether`` (tokensAmount: int) =
//     restore ()

//     let debug = Debug(EthereumConnection(hardhatURI, makeAccountWithBalance().PrivateKey)) // the balance is needed for gas vs for sending ether value.
//     let data = dEthContract.redeemData(makeAccount().Address, bigint tokensAmount)
    
//     debug.Forward(dEthContract.Address, data)
//     |> debug.DecodeForwardedEvents
//     |> Seq.head
//     |> shouldRevertWithMessage "ERC20: burn amount exceeds balance"

// [<Specification("dEth", "squanderMyEthForWorthlessBeansAndAgreeToTerms", 1)>]
// [<Theory>]
// [<InlineData(100.0)>]
// [<InlineData(10.0)>]
// [<InlineData(1.0)>]
// [<InlineData(0.01)>]
// [<InlineData(0.001)>]
// [<InlineData(0.0001)>]
// [<InlineData(0.0)>] // a test case checking that no-one providing no ether can issue themselves any dEth
// let ``dEth - squanderMyEthForWorthlessBeansAndAgreeToTerms - anyone providing a positive balance of Ether can issue themselves the expected amount of dEth`` (providedCollateral:float) =
//     restore ()

//     let providedCollateralBigInt = bigint providedCollateral

//     let inkBefore = getInk ()
//     let gulperBalanceBefore = getGulperEthBalance ()
    
//     dEthContract.getExcessCollateralQuery()
//     |> should lessThan (dEthContract.riskLimitQuery() + providedCollateralBigInt)
    
//     let (protocolFeeExpected, automationFeeExpected, actualCollateralAddedExpected, accreditedCollateralExpected, tokensIssuedExpected) = 
//         queryStateAndCalculateIssuanceAmount dEthContract providedCollateralBigInt

//     let dEthRecipientAddress = ethConn.Account.Address
//     let balanceBefore = balanceOf dEthContract dEthRecipientAddress

//     let squanderTxr = dEthContract.squanderMyEthForWorthlessBeansAndAgreeToTerms(dEthRecipientAddress, weiValue providedCollateralBigInt)
//     squanderTxr |> shouldSucceed

//     balanceOf dEthContract dEthRecipientAddress |> should equal (balanceBefore + tokensIssuedExpected)
//     getInk () |> should equal (inkBefore + actualCollateralAddedExpected)
//     getGulperEthBalance () |> should equal (gulperBalanceBefore + protocolFeeExpected)

//     let issuedEvent = squanderTxr.DecodeAllEvents<Contracts.dEthContract.IssuedEventDTO>() |> Seq.map (fun i -> i.Event) |> Seq.head

//     issuedEvent._receiver |> shouldEqualIgnoringCase dEthRecipientAddress
//     issuedEvent._suppliedCollateral |> should equal providedCollateralBigInt
//     issuedEvent._protocolFee |> should equal protocolFeeExpected
//     issuedEvent._automationFee |> should equal automationFeeExpected
//     issuedEvent._actualCollateralAdded |> should equal actualCollateralAddedExpected
//     issuedEvent._accreditedCollateral |> should equal accreditedCollateralExpected
//     issuedEvent._tokensIssued |> should equal tokensIssuedExpected

// [<Specification("dEth", "squanderMyEthForWorthlessBeansAndAgreeToTerms", 2)>]
// [<Fact>]
// let ``dEth - squanderMyEthForWorthlessBeansAndAgreeToTerms - the riskLevel cannot be exceeded`` () =
//     restore ()

//     makeRiskLimitLessThanExcessCollateral dEthContract |> shouldSucceed

//     let data = dEthContract.squanderMyEthForWorthlessBeansAndAgreeToTermsData(makeAccount().Address)
//     debug.Forward(dEthContract.Address, data)
//     |> debug.DecodeForwardedEvents
//     |> Seq.head
//     |> shouldRevertWithMessage "risk limit exceeded"