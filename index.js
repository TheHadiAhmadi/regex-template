const template = `
<div class="flex">
<Component dynamic attr1="value1" attr2="value2" attr3></Component>
<Component dynamic battr1="bvalue1" battr2="bvalue2" attr3></Component>
</div>
`;

const expected = [
  {
    attr1: "value1",
    attr2: "value2",
    attr3: true
  },
  {
    battr1: "bvalue1",
    battr2: "bvalue2",
    attr3: true
  },
];

async function run(template) {
  const result = [];

  const regex = /<\s*([a-zA-Z0-9]+)\s*dynamic\s*([^>]*)\s*>/g;

  const attributesStrList = [...template.matchAll(regex)].map((x) => x[2]);
  // const
  for (let attributesStr of attributesStrList) {
    const attributes = {};

    // Define a regular expression pattern to match attribute key-value pairs
    const attributePattern = /(\w+)(?:="([^"]*)")?/g;

    // Initialize an object to store the extracted attributes

    // Iterate over attribute matches
    let match;
    while ((match = attributePattern.exec(attributesStr)) !== null) {
      const key = match[1];
      const value = match[2] || true; // If there's no value, set it to true

      // Add the key-value pair to the object
      attributes[key] = value;
    }

    result.push(attributes)
  }

  return result;
}

run(template).then((result) => {
  console.log(result);
  console.log(expected);
});
