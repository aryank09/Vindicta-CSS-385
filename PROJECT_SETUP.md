# Vindicta Unity Game Project Website Setup Guide

This guide will help you set up and customize your complete Unity game project website.

## 📁 Project Structure

Your project now has the following structure:
```
Vindicta-CSS-385/
├── index.html              # Main website file
├── styles.css              # All styling
├── script.js               # Interactive features
├── README.txt              # Unity project info
├── PROJECT_SETUP.md        # This guide
├── screenshots/            # Game screenshots folder
│   └── README.md          # Screenshot guidelines
├── presentations/          # Slide presentations
├── assets/                # Additional assets
└── docs/                  # Documentation
    ├── content-guide.md   # Content filling guide
    └── playtesting-template.md # Playtesting help
```

## 🚀 Quick Start

### 1. Host Your Website

**Option A: GitHub Pages (Recommended)**
1. Push all files to your GitHub repository
2. Go to Settings > Pages
3. Select "Deploy from a branch" 
4. Choose "main" branch and "/ (root)"
5. Your site will be available at: `https://yourusername.github.io/repository-name/`

**Option B: Other Hosting**
- Upload all files to your web hosting service
- Make sure `index.html` is in the root directory

### 2. Add Your Content

Replace all placeholders marked with `[INSERT...]` in `index.html`:

#### Essential Information:
- **Game title**: Replace "Vindicta" with your actual game name
- **University name**: Update the footer with your school
- **Team information**: Add all team members in the Credits section
- **Game description**: Fill in genre, mechanics, and unique features

#### Media Assets:
- **Screenshots**: Add 5 images to `/screenshots/` folder
- **Trailer**: Upload to YouTube and get the video ID
- **Unity builds**: Add WebGL build and desktop downloads
- **Presentations**: Upload your postmortem and final presentations

## 📋 Content Checklist

Use this checklist to ensure you've completed all 11 required sections:

### ✅ 1. Game Description Section
- [ ] Genre and gameplay concept
- [ ] Influences and inspiration  
- [ ] Unique features (4 bullet points)
- [ ] Technical specifications
- [ ] Launch date and rating

### ✅ 2. Playable Game Embed
- [ ] WebGL build uploaded and embedded
- [ ] Windows download link (optional)
- [ ] Mac download link (optional)
- [ ] File sizes updated

### ✅ 3. Playtesting Reports
- [ ] Early development playtest (Week 3)
- [ ] Mid-development playtest (Week 6)  
- [ ] Final playtest with 10+ players
- [ ] Specific feedback and changes documented
- [ ] Future recommendations included

### ✅ 4. Credits Section
- [ ] All team members listed
- [ ] Roles clearly defined
- [ ] Contact emails (optional)
- [ ] Key contributions for each member

### ✅ 5. References Section
- [ ] All external assets listed
- [ ] Source URLs provided
- [ ] License types confirmed
- [ ] "Free for non-commercial use" verified

### ✅ 6. Game Trailer Section
- [ ] YouTube video uploaded (30-60 seconds)
- [ ] Video ID added to embed code
- [ ] Trailer description written

### ✅ 7. Screenshots Section
- [ ] 5 representative screenshots added
- [ ] Images properly named and sized
- [ ] Captions for each screenshot
- [ ] Files under 2MB each

### ✅ 8. Source Code Section
- [ ] GitHub repository link
- [ ] Download ZIP option
- [ ] README.txt updated with Unity version
- [ ] Website URL added

### ✅ 9. Postmortem Presentation
- [ ] 2-slide presentation created
- [ ] 5 things that went right
- [ ] 5 things that went wrong
- [ ] Presentation embedded or linked

### ✅ 10. Game Presentation  
- [ ] Final 5-slide presentation from demo day
- [ ] Title and summary included
- [ ] Key game features showcased
- [ ] Presentation embedded or linked

### ✅ 11. Team Reflection
- [ ] "What did your team do well?" with evidence
- [ ] "Top 3 priorities for 2 more weeks?" with explanations
- [ ] Honest and specific responses

## 🎨 Customization Options

### Colors and Branding
Edit the CSS custom properties in `styles.css`:
```css
:root {
    --primary-color: #64ffda;    /* Accent color */
    --secondary-color: #667eea;  /* Secondary accent */
    --dark-bg: #1a1a1a;         /* Dark backgrounds */
    --light-bg: #f8f9fa;        /* Light backgrounds */
}
```

### Fonts
Current font stack: `Inter, Arial, sans-serif`
To change fonts, update the Google Fonts link in `index.html` and the CSS font-family properties.

### Layout
- Modify grid layouts in CSS for different arrangements
- Adjust section padding and spacing as needed
- Change breakpoints for mobile responsiveness

## 🔧 Technical Features

### Built-in Functionality
- ✅ Smooth scrolling navigation
- ✅ Responsive design for all devices
- ✅ Image modal for screenshot viewing
- ✅ Hover effects and animations
- ✅ Error handling for missing media
- ✅ Loading animations
- ✅ Performance monitoring

### Optional Enhancements
- Add Google Analytics for visitor tracking
- Implement contact forms
- Add social media sharing buttons
- Include a blog section for development updates

## 📱 Testing Your Website

### Before Publishing:
1. **Test all links**: Ensure navigation and external links work
2. **Check media**: Verify all images and videos load properly
3. **Mobile testing**: Test on different screen sizes
4. **Performance**: Check loading times (aim for <3 seconds)
5. **Accessibility**: Test with screen readers if possible
6. **Cross-browser**: Test in Chrome, Firefox, Safari, and Edge

### Common Issues:
- **Images not loading**: Check file paths and names
- **Broken links**: Verify all URLs and anchors
- **Layout issues**: Test responsive design on mobile
- **Slow loading**: Optimize image sizes (under 2MB each)

## 📊 Analytics and Feedback

### Track Your Success:
- Monitor website visits with Google Analytics
- Collect feedback from classmates and instructors
- Note which sections get the most attention
- Document any technical issues encountered

### Continuous Improvement:
- Update content based on feedback
- Fix any bugs or broken links
- Add new features as you learn more
- Keep documentation up to date

## 🎯 Grading Considerations

### What Professors Look For:
1. **Completeness**: All 11 sections properly filled out
2. **Quality**: Professional presentation and clear writing
3. **Functionality**: Working links, proper embeds, responsive design
4. **Evidence**: Specific examples and concrete evidence in reflections
5. **Accessibility**: Clean, readable design with proper navigation

### Pro Tips:
- Use specific numbers and quotes in playtesting reports
- Include concrete evidence in team reflections
- Ensure all external assets are properly licensed
- Proofread all content for spelling and grammar
- Test the website on different devices before submission

## 🆘 Troubleshooting

### Common Problems and Solutions:

**"My WebGL build won't load"**
- Check file paths are correct
- Ensure all WebGL files are uploaded
- Verify your hosting service supports the file types

**"Images are broken"**
- Confirm file names match exactly (case-sensitive)
- Check image file extensions (.png, .jpg)
- Verify files are in the correct folders

**"YouTube embed not working"**
- Make sure video is public or unlisted
- Use the correct video ID from the URL
- Test the embed code in a simple HTML file first

**"Website looks broken on mobile"**
- Clear browser cache and reload
- Test CSS media queries
- Check for any CSS syntax errors

### Getting Help:
- Check the browser console for error messages
- Validate your HTML and CSS using online validators
- Ask classmates to test your website
- Consult with your instructor during office hours

---

## 📝 Final Notes

This website template provides a professional foundation for your Unity game project. Take time to customize it properly and fill in all required content. The more effort you put into the details, the better your final grade and portfolio piece will be.

Remember: This website can serve as a portfolio piece for future job applications, so make it shine! 🌟

Good luck with your project! 🎮 